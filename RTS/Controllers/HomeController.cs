﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RTS.BusinessLogic.IServices;
using RTS.BusinessLogic.ViewModel;
using RTS.DataAccess.Data;
using RTS.DataAccess.Logic.RTSEntities;
using RTS.Models;

namespace RTS.Controllers
{
    public class HomeController : BaseController
    {
        private readonly RTSDBContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IItemService _itemServices;
        private readonly IItemRequestService _itemRequestService;
        private readonly ITransectionService _transactionService;
        private readonly UserManager<Employee> _userManager;

        public HomeController(ILogger<HomeController> logger, IItemService itemServices,UserManager<Employee> userManager,
            IItemRequestService itemRequestService, ITransectionService transactionService,RTSDBContext context)
        {
            _context = context;
            _logger = logger;
            _itemServices = itemServices;
            _userManager = userManager;
            _itemRequestService = itemRequestService;
            _transactionService = transactionService;
        }

        public async Task<IActionResult> Index(string search)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var item =_itemServices.GetItemsByName(search);
            var it = new ItemViewModel();
            it.items = item;

            if (user != null)
            {
                var userItems = _itemServices.GetItemsByUser(user);
                it.Devices = userItems;
            }

            if (TempData["status"] != null)
            {
                ViewBag.message = TempData["status"].ToString();
                TempData.Remove("status");
            }

            return View(it);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMail(int id, [Bind("Id,DeviceTypeId,Name,Description,Manufacturer,Model,SerialNumber,CurentUserId,IsActive,IsDeleted,PurchaseDate")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var itemUser = await _userManager.FindByIdAsync(item.CurentUserId);

            if (await _userManager.IsInRoleAsync(user, "Employee") && itemUser.Email != "admin@i.com" && itemUser.Email != null&&user.Id!=itemUser.Id)
            {
               string body=_itemRequestService.EmailText("wwwroot/Mail/Mail.html");
               Boolean result= _itemRequestService.SendRequest(itemUser,body,item.Id);
                if (result)
                {
                    var itemRequest=await _itemRequestService.Create(item.Id, itemUser.Email, user.Id ,3);
                    await _transactionService.Create(itemRequest.Id, item.DeviceTypeId, DateTime.Now);

                    TempData["status"] = "Success";
                }
                else
                {
                    TempData["status"] = "Failed";
                }
            }

            else if(itemUser.Email == "admin@i.com"&& user.Id != itemUser.Id)
            {  
                item.CurentUserId = user.Id;
                await _itemServices.Edit(item);

                var itemRequest= await _itemRequestService.Create(item.Id, itemUser.Email, user.Id,1);

                await _transactionService.Create(itemRequest.Id, item.DeviceTypeId, DateTime.Now);


                TempData["status"] = "Success";
             
            }
            else
            {
                TempData["status"] = "Failed";
            }
          
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(int id, [Bind("Id,DeviceTypeId,Name,Description,Manufacturer,Model,SerialNumber,CurentUserId,IsActive,IsDeleted,PurchaseDate")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }
            var user = await _userManager.FindByNameAsync("admin@i.com");
            var user1 = await _userManager.FindByIdAsync(item.CurentUserId);

            var itemRequest = await _itemRequestService.Create(item.Id, user1.Email, user.Id, 4);
            await _transactionService.Create(itemRequest.Id, item.DeviceTypeId, DateTime.Now);

            item.CurentUserId = user.Id;
            await _itemServices.Edit(item);
            TempData["status"] = "returned";

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Approve(int? id,string userId)
        {
            var item = await _context.Items.FindAsync(id);
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var user1 = await _userManager.FindByIdAsync(item.CurentUserId);
            if (user.Id != user1.Id && userId==user1.Id )
            {
                var itemRequest = await _itemRequestService.Create(item.Id, user1.Email, user.Id, 1);
                await _transactionService.Create(itemRequest.Id, item.DeviceTypeId, DateTime.Now);

                item.CurentUserId = user.Id;
                await _itemServices.Edit(item);
                ViewBag.success = "Item Have been Send Successfuly";

                return View();
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Deny(int? id, string userId)
        {
            var item = await _context.Items.FindAsync(id);
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var user1 = await _userManager.FindByIdAsync(item.CurentUserId);
            if (user.Id != item.CurentUserId && userId == user1.Id)
            {
                var itemRequest = await _itemRequestService.Create(item.Id, user1.Email, user.Id, 3);
                await _transactionService.Create(itemRequest.Id, item.DeviceTypeId, DateTime.Now);
                ViewBag.denied = "Item Have been Denied";

                return View();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

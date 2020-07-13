using System;
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
using RTS.DataAccess.Logic.RTSEntities;
using RTS.Models;

namespace RTS.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IItemService _itemServices;
        private readonly IItemRequestService _itemRequestService;
        private readonly UserManager<Employee> _userManager;


        public HomeController(ILogger<HomeController> logger, IItemService itemServices,UserManager<Employee> userManager, IItemRequestService itemRequestService)
        {
            _logger = logger;
            _itemServices = itemServices;
            _userManager = userManager;
            _itemRequestService = itemRequestService;
        }
        public async Task<IActionResult> Index(string search)
        {
            if (TempData["status"] != null)
            {
                ViewBag.message = TempData["status"].ToString();
                TempData.Remove("status");
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if(user != null)
            {
                var userItems = _itemServices.GetItemsByUser(user);
            }
            var item =_itemServices.GetItemsByName(search);
            return View(item);
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
            if (await _userManager.IsInRoleAsync(user, "Employee") && itemUser.Email != "admin@i.com" && itemUser.Email != null)
            {
               Boolean result= _itemRequestService.RequestItem(itemUser);
                if (result)
                {
                    TempData["status"] = "Success";
                }
                else
                {
                    TempData["status"] = "Failed";
                }
            }
            else if(itemUser.Email == "admin@i.com")
            {
                item.CurentUserId = user.Id;
                await _itemServices.Edit(item);
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
            var itemUser = await _userManager.FindByNameAsync("admin@i.com");

            item.CurentUserId = itemUser.Id;
            await _itemServices.Edit(item);

            TempData["status"] = "returned";

            return RedirectToAction(nameof(Index));
        }

    }
}

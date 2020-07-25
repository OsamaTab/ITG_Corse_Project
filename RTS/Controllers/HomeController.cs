using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> Index(string? search,int? filter)
        {
            if (TempData["success"] != null)
            {
                ViewBag.success = TempData["success"].ToString();
                TempData.Remove("success");
            }
            if (TempData["failed"] != null)
            {
                ViewBag.failed = TempData["failed"].ToString();
                TempData.Remove("failed");
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var item =_itemServices.GetItemsByName(search,filter);
            var it = new ItemViewModel();
            it.items = item;
            ViewBag.Email = user.Email;

            if (user != null)
            {
                var userItems = _itemServices.GetItemsByUser(user);
                it.Devices = userItems;
            }
            var request = _itemRequestService.GetRequestByUser(user.Email);
            it.Request = request;
            ViewData["DeviceTypeId"] = new SelectList(_context.ItemCategories, "Id", "Type");
            return View(it);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendRequest(int? id)
        {
            var item = await _context.Items.FindAsync(id);
            if (id != item.Id)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var itemUser = await _userManager.FindByIdAsync(item.CurentUserId);

            if (await _userManager.IsInRoleAsync(user, "Employee") &&  itemUser.Email != null&&user.Id!=itemUser.Id)
            {
               string body=_itemRequestService.EmailText("wwwroot/Mail/Request.html");
               var itemRequest=await _itemRequestService.Create(item.Id, itemUser.Email, user.Id ,2);
               _itemRequestService.SendRequest(itemUser.Email, body,itemRequest.Id);
               await _transactionService.Create(itemRequest.Id, item.DeviceTypeId, DateTime.Now);
               TempData["success"] = "Email Have Been Success";
               
            }

            //else if(itemUser.Email == "admin@i.com"&& user.Id != itemUser.Id)
            //{  
            //    item.CurentUserId = user.Id;
            //    await _itemServices.Edit(item);

            //    var itemRequest= await _itemRequestService.Create(item.Id, itemUser.Email, user.Id,1);

            //    await _transactionService.Create(itemRequest.Id, item.DeviceTypeId, DateTime.Now);


            //    TempData["success"] = "Item is Added To Your Devices";
             
            //}
            else
            {
                TempData["failed"] = "Email Failed To Send";
            }
          
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(int? id)
        {
            var item = await _context.Items.FindAsync(id);
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
            TempData["success"] = "Item Has Been Returned";

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Approve(int?id ,int?model)
        {
            var request =await _context.ItemRequests.FindAsync(id);
            var item = await _context.Items.FindAsync(request.ItemId);
            var user = await _userManager.FindByIdAsync(request.RequestedUserId);
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            
            string body = _itemRequestService.EmailText("wwwroot/Mail/Response.html");
            if (model != null && id!=null && request.StatusId==2)
            {
                var itemRequest = await _itemRequestService.Edit(request.Id, 1);
                await _transactionService.Create(itemRequest.Id, item.DeviceTypeId, DateTime.Now);
                item.CurentUserId = request.RequestedUserId;
                await _itemServices.Edit(item);
                _itemRequestService.SendResponse(user.Email, body, "Approved");
                TempData["success"] = "Item Have been Send Successfuly";
            }
            else if(model==null && id != null && currentUser.Id == user.Id && request.StatusId == 2)
            {
                var itemRequest = await _itemRequestService.Edit(request.Id, 1);
                await _transactionService.Create(itemRequest.Id, item.DeviceTypeId, DateTime.Now);
                item.CurentUserId = request.RequestedUserId;
                await _itemServices.Edit(item);
                ViewBag.success = "Item Have been Send Successfuly";
                _itemRequestService.SendResponse(user.Email, body, "Approved");
                return View();
            }
            else
            {
                TempData["failed"] = "Failed To approve";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Deny(int? id)
        {
            var request =await _context.ItemRequests.FindAsync(id);
            ViewBag.id = request.Id;
            ViewBag.userId = request.RequestedUser;
            return View(); 
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Deny(int? id, string? message,int?model)
        {
            var request = await _context.ItemRequests.FindAsync(id);
            var item = await _context.Items.FindAsync(request.ItemId);
            var user = await _userManager.FindByIdAsync(request.RequestedUserId);
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            string body = _itemRequestService.EmailText("wwwroot/Mail/Response.html");

            if (model != null && request.StatusId == 2)
            {
                var itemRequest = await _itemRequestService.Edit(request.Id, 3);
                await _transactionService.Create(itemRequest.Id, item.DeviceTypeId, DateTime.Now);
                _itemRequestService.SendResponse(user.Email, body, "Rejected Because " + message);
                TempData["success"] = "Your Message Have Been Send Successfuly";
            }

            else if (model==null && currentUser.Id == user.Id && request.StatusId == 2)
            {
                var itemRequest = await _itemRequestService.Edit(request.Id, 3);
                await _transactionService.Create(itemRequest.Id, item.DeviceTypeId, DateTime.Now);
                _itemRequestService.SendResponse(user.Email, body, "Rejected Because " + message);
                
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

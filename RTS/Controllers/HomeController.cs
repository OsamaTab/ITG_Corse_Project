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
using RTS.BusinessLogic.ViewModel;
using RTS.DataAccess.Logic.RTSEntities;
using RTS.Models;

namespace RTS.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IItemService _itemServices;
        private readonly IItemRequestService _itemRequestService;
        private readonly ITransectionService _transactionService;
        private readonly UserManager<Employee> _userManager;


        public HomeController(ILogger<HomeController> logger, IItemService itemServices,UserManager<Employee> userManager,
            IItemRequestService itemRequestService, ITransectionService transactionService)
        {
            _logger = logger;
            _itemServices = itemServices;
            _userManager = userManager;
            _itemRequestService = itemRequestService;
            _transactionService = transactionService;
        }
        public async Task<IActionResult> Index(string search)
        {
            if (TempData["status"] != null)
            {
                ViewBag.message = TempData["status"].ToString();
                TempData.Remove("status");
            }

            var item =_itemServices.GetItemsByName(search);
            var it = new ItemViewModel();
            it.items = item;

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                var userItems = _itemServices.GetItemsByUser(user);
                it.Devices = userItems;
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

            if (await _userManager.IsInRoleAsync(user, "Employee") && itemUser.Email != "admin@i.com" && itemUser.Email != null)
            {
               string body=_itemRequestService.EmailText("wwwroot/Mail/Mail.html");
               Boolean result= _itemRequestService.SendRequest(itemUser,body);
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
                try
                {
                    item.CurentUserId = user.Id;
                    await _itemServices.Edit(item);

                    ItemRequest itemRequest = new ItemRequest
                    {
                        ItemId = item.Id,
                        ItemOwner = itemUser.Email,
                        RequestedUserId=user.Id,
                        StatusId=1
                    };

                    await _itemRequestService.Create(itemRequest);

                    Trnasaction transaction = new Trnasaction
                    {
                       ItemRequestId=itemRequest.Id,
                       DeviceTypeId=item.DeviceTypeId,
                       TransectionDate=DateTime.Now
                    };
                    await _transactionService.Create(transaction);


                    TempData["status"] = "Success";
                }
                catch (Exception)
                {
                    TempData["status"] = "Failed";
                }
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

            ItemRequest itemRequest = new ItemRequest
            {
                ItemId = item.Id,
                ItemOwner = user1.Email,
                RequestedUserId = user.Id,
                StatusId = 4
            };

            await _itemRequestService.Create(itemRequest);

            Trnasaction transaction = new Trnasaction
            {
                ItemRequestId = itemRequest.Id,
                DeviceTypeId = item.DeviceTypeId,
                TransectionDate = DateTime.Now
            };
            await _transactionService.Create(transaction);

            item.CurentUserId = user.Id;
            await _itemServices.Edit(item);
            TempData["status"] = "returned";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deny(int id)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}

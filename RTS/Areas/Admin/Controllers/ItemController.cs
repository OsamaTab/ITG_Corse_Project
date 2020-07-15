using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RTS.BusinessLogic.IServices;
using RTS.DataAccess.Data;
using RTS.DataAccess.Logic.RTSEntities;

namespace RTS.Areas.Admin.Controllers
{

    public class ItemController : BaseController
    {
        private readonly RTSDBContext _context;
        private readonly IItemService _itemServices;
        private readonly UserManager<Employee> _userManager;



        public ItemController(RTSDBContext context, IItemService itemServices, UserManager<Employee> userManager)
        {
            _context = context;
            _itemServices = itemServices;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var item = _itemServices.GetItems() ;
            return View(item);
        }
        public async Task<IActionResult> Deleted()
        {
            var item = _itemServices.GetDeletedItems();
            return View(item);
        }

        public IActionResult Create()
        {
            ViewData["DeviceTypeId"] = new SelectList(_context.ItemCategories, "Id", "Type");
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeviceTypeId,Name,Description,Manufacturer,Model,SerialNumber,IsActive")] Item item)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                item.CurentUserId = user.Id;

                await _itemServices.Create(item);
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeviceTypeId"] = new SelectList(_context.ItemCategories, "Id", "Type");
            return View(item);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["DeviceTypeId"] = new SelectList(_context.ItemCategories, "Id", "Type", item.DeviceTypeId);
            return View(item);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DeviceTypeId,Name,Description,Manufacturer,Model,SerialNumber,IsActive,IsDeleted,CurentUserId,PurchaseDate")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _itemServices.Edit(item);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _itemServices.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}

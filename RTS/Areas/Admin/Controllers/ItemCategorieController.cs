using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RTS.BusinessLogic.IServices;
using RTS.DataAccess.Data;
using RTS.DataAccess.Logic.RTSEntities;

namespace RTS.Areas.Admin.Controllers
{
    public class ItemCategorieController : BaseController
    {
        private readonly IItemCategorieService _itemCategorieService;
        private readonly RTSDBContext _context;

        public ItemCategorieController(IItemCategorieService itemCategorieService, RTSDBContext context)
        {
            _itemCategorieService = itemCategorieService;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var model = _itemCategorieService.GetItemCategories();

            return View("Areas/Admin/Views/ItemCategorie/Index.cshtml",model);
        }
        public async Task<IActionResult> Deleted()
        {
            var model = _itemCategorieService.GetDeletedItemCategories();

            return View(model);
        }

        public IActionResult Create()
        {
            return View("Areas/Admin/Views/ItemCategorie/Create.cshtml");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type")] ItemCategorie itemCategorie)
        {
            if (ModelState.IsValid)
            {
                await _itemCategorieService.Create(itemCategorie);
                return RedirectToAction(nameof(Index));
            }
            return View(itemCategorie);
        }

    
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemCategorie = await _context.ItemCategories.FindAsync(id);
            if (itemCategorie == null)
            {
                return NotFound();
            }
            return View("Areas/Admin/Views/ItemCategorie/Edit.cshtml", itemCategorie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,IsDeleted")] ItemCategorie itemCategorie)
        {
            if (id != itemCategorie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _itemCategorieService.Edit(itemCategorie);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemTypeExists(itemCategorie.Id))
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
            return View(itemCategorie);
        }

   
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _itemCategorieService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ItemTypeExists(int id)
        {
            return _context.ItemCategories.Any(e => e.Id == id);
        }
    }
}

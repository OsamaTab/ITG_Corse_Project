using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RTS.BusinessLogic.IServices;
using RTS.BusinessLogic.ViewModel;
using RTS.DataAccess.Data;
using RTS.DataAccess.Logic.RTSEntities;

namespace RTS.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly RTSDBContext _context;
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountController(IAccountService accountService, RTSDBContext context, UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager)
        {
            _accountService = accountService;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var model =await _accountService.GetEmployee();

            return View("Areas/Admin/Views/Account/Index.cshtml", model);
        }
        public async Task<IActionResult> Deleted()
        {
            var model = await _accountService.GetDeletedEmployee();

            return View( model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");

            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            var model = new UserViewModel
            {
                UserId = user.Id,
                UserName = user.Email,
                IsDeleted=user.IsDeleted

            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string userId, UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _accountService.Edit(userId, model);
                return RedirectToAction("index", "account");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            await _accountService.Delete(id);
            return RedirectToAction("index", "account");
        }
    }
}


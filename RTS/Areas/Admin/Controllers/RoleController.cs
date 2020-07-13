using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RTS.BusinessLogic.IServices;

namespace RTS.Areas.Admin.Controllers
{
    public class RoleController : BaseController
    {
        private readonly IAccountService _accountService;
        

        public RoleController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public async Task<IActionResult> Index()
        {
            var model = _accountService.GetRole();

            return View("Areas/Admin/Views/Role/Index.cshtml", model);
        }

   
}
}

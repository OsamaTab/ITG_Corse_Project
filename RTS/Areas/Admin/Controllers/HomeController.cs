using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RTS.BusinessLogic.IServices;
using RTS.DataAccess.Data;

namespace RTS.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ITransectionService _transactionService;
        private readonly RTSDBContext _context;

        public HomeController(ITransectionService transectionService,RTSDBContext context)
        {
            _transactionService = transectionService;
            _context = context;
        }
        public async Task<IActionResult> Index(string? search,int? filter)
        {
            ViewData["StatusId"] = new SelectList(_context.RequestStatuses, "Id", "Status");

            var model =_transactionService.GetTransections(search,filter);
            return View(model);
        }
    }
}

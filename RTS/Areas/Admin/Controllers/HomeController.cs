using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RTS.BusinessLogic.IServices;

namespace RTS.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ITransectionService _transactionService;
        public HomeController(ITransectionService transectionService)
        {
            _transactionService = transectionService;
        }
        public IActionResult Index()
        {
            var model=_transactionService.GetTransections();
            return View(model);
        }
    }
}

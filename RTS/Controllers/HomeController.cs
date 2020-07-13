using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RTS.BusinessLogic.IServices;
using RTS.Models;

namespace RTS.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IItemService _itemServices;

        public HomeController(ILogger<HomeController> logger, IItemService itemServices)
        {
            _logger = logger;
            _itemServices = itemServices;
        }
        public IActionResult Index(string search)
        {
            var item=_itemServices.GetItemsByName(search);
            return View(item);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

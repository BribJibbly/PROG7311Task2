using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrdersAppTask2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersAppTask2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Log()
        {
            if(HttpContext.Session.GetString("LoggedInUser") != null)
            {
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                return View();
            }

        }
        public IActionResult Transaction()
        {
            if (HttpContext.Session.GetString("LoggedInUser") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Privacy", "Home");
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

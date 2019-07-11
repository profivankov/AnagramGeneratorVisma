using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers
{
    public class CookiesController : Controller
    {
        public IActionResult Cookies()
        {
            return View();
        }
        public IActionResult UserLog()
        {
            return View();
        }
    }
}
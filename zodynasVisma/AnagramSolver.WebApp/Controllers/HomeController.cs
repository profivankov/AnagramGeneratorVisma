using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnagramSolver.WebApp.Models;
using System.Text.Encodings.Web;
using AnagramSolver.BusinessLogic;
using System.IO;
using System.Configuration;

namespace AnagramSolver.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private string[] routeInput;


        public BusinessLogic.AnagramSolver anagramObject { get;  set; }
        public FileWordRepository dictonaryObject { get; set; }
        StreamReader file;


        public HomeController()
        {
            routeInput = new string[] { "" };
            file = new StreamReader(@"C:\Users\mantrimas\source\repos\zodynasVisma\zodynasVisma\zodynas.txt");
        }

        public IActionResult Index()
        {
            return new EmptyResult();
        }

        [Route("Home/Index/")]
        public IActionResult Index(string[] word)
        {
            bool success = int.TryParse(ConfigurationManager.AppSettings["MaxResultAmount"], out var _defaultValue);
            if (!success)
            {
                _defaultValue = 100;
            }
            routeInput = word ;
            anagramObject = new BusinessLogic.AnagramSolver(new FileWordRepository(), _defaultValue);
            var resultList = new AnagramViewModel { wordList = anagramObject.GetAnagrams(routeInput, file) };
            return View(resultList);
        }


        public IActionResult Dictionary()
        {
            dictonaryObject = new FileWordRepository();
            var list = dictonaryObject.GetDictionary(file).Values.SelectMany(x => x).ToList();           

            var pager = new DictionaryViewModel(totalItems: 150);

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

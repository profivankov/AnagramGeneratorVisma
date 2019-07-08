using AnagramSolver.BusinessLogic;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AnagramSolver.WebApp.Controllers
{
    public class AnagramSolverController : Controller
    {
        public BusinessLogic.AnagramSolver anagramObject { get;  set; }
        StreamReader file;
        
        public AnagramSolverController()
        {
            file = new StreamReader(@"C:\Users\mantrimas\source\repos\zodynasVisma\zodynasVisma\zodynas.txt");
        }

        public IActionResult Index() // must return this if user doesnt submit input, also find way for app to load index instead of blank localhost page
        {
            return View(new AnagramViewModel { WordList = new List<string>() });
        }

        [Route("AnagramSolver/Index/{word?}")] // kam sitas reikalingas jeigu startup.cs apibrezti routes?
        public IActionResult Index(AnagramViewModel request, string word)  
        {

            if (!string.IsNullOrEmpty(word))
            {
                
                request.Input = word.Split(" ");
            }

            if (request.Input == null || request.Input.Length == 0)
            {
                return Index();
            }

            //string someword = request.Input.ToString();


            
  

            RouteData.Values.Remove("word"); // removes url parameter 
            var input = string.Join(" ", request.Input);
            Response.Cookies.Append("searchedWord", input); // add cookie
            var splitInput = input.Split(" "); // put the strings seperated by spaces into an array so they can be passed to the GetAnagrams function // need to find a better workaround

            bool success = int.TryParse(ConfigurationManager.AppSettings["MaxResultAmount"], out var _defaultValue);
            if (!success)
            {
                _defaultValue = 100;
            }
            anagramObject = new BusinessLogic.AnagramSolver(new FileWordRepository(), _defaultValue);
            var resultList = new AnagramViewModel { WordList = anagramObject.GetAnagrams(splitInput, file) };
            return View(resultList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

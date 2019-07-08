﻿using AnagramSolver.BusinessLogic;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;





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

      

        //[Route("Home/Index")]
        public IActionResult Index() // must return this if user doesnt submit input, also find way for app to load index instead of blank localhost page
        {
            return View( new AnagramViewModel { wordList = new List<string>() } );
        }

        [Route("Home/Index/{word?}")]
        public IActionResult Index(AnagramViewModel request, string word)  
        {

            //var checkWord = string.Join(" ", word);
            //var fixedWord = word.Split(" ");

            if (!string.IsNullOrEmpty(word))
            {
                
                request.input = word.Split(" ");
                //Response.Redirect(checkWord);
                //Index(request, request.input);
            }


            if (request.input == null || request.input.Length == 0)
            {
                return Index();
            }

            RouteData.Values.Remove("word"); // removes url parameter 
            var input = string.Join(" ", request.input);

            //string words = string.Join(" ", input); // transform string array into single string seperated by spaces (original input has all strings in first index of the array)
            var splitInput = input.Split(" "); // put the strings seperated by spaces into an array so they can be passed to the GetAnagrams function // need to find a better workaround

            bool success = int.TryParse(ConfigurationManager.AppSettings["MaxResultAmount"], out var _defaultValue);
            if (!success)
            {
                _defaultValue = 100;
            }
            routeInput = splitInput;
            anagramObject = new BusinessLogic.AnagramSolver(new FileWordRepository(), _defaultValue);
            var resultList = new AnagramViewModel { wordList = anagramObject.GetAnagrams(routeInput, file) };
            return View(resultList);
        }

        //[HttpGet]
        //[Route("Home/Dictionary?pageNum={pageNum}")]
        public IActionResult Dictionary(int pageNum)
        {
            dictonaryObject = new FileWordRepository();
            var list = dictonaryObject.GetDictionary(file).Values.SelectMany(x => x); //.ToList();

            var pager = new DictionaryViewModel(list.Count(), pageNum);

            var viewModel = new IndexViewModel
            {
                Items = list.Skip((pageNum-1)*100).Take(pager.PageSize),
                Pager = pager
            };

            return View(viewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

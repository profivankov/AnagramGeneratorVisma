
using AnagramSolver.Contracts;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;


namespace AnagramSolver.WebApp.Controllers
{
    public class AnagramSolverController : Controller
    {
        private readonly IAnagramSolver anagramSolver;
        private ICacheRepository anagramCache;

        public AnagramSolverController(IAnagramSolver anagramSolver, ICacheRepository anagramCache)
        {
            this.anagramSolver = anagramSolver;
            this.anagramCache = anagramCache;
        }

        public IActionResult Index() 
        {
            return View(new AnagramViewModel { WordList = new List<string>() });
        }

        [Route("AnagramSolver/Index/{word?}")] // kam sitas reikalingas jeigu startup.cs apibrezti routes? kad passint argument
        public IActionResult Index(AnagramViewModel request)  
        {
            if (request.Input == null || request.Input.Length == 0)
            {
                return Index();
            }

            var input = string.Join(" ", request.Input);
            Response.Cookies.Append("searchedWord", input); // add cookie
            var splitInput = input.Split(" "); // put the strings seperated by spaces into an array so they can be passed to the GetAnagrams function // need to find a better workaround

            var cacheList = anagramCache.SearchCacheForAnagrams(input);
            var resultList = new AnagramViewModel();
            if (cacheList.Count == 0) // if words aren't cached use getanagrams and cache
            {
                resultList = new AnagramViewModel { WordList = anagramSolver.GetAnagrams(splitInput) };
                foreach (string s in splitInput)
                {
                    CacheWords(resultList.WordList, s); 
                }
            }
            else
            {
                resultList.WordList = cacheList;
            }


            return View(resultList);
        }

        public IActionResult CacheWords(IList<string> list, string word) // find anagram ID's in DB and put them in a different table
        {
            anagramCache.AddCacheToRepository(list, word);
            return null;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


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

        [Route("AnagramSolver/Index/")] // kam sitas reikalingas jeigu startup.cs apibrezti routes? kad passint argument
        public IActionResult Index(AnagramViewModel request)  
        {
            if (request.Input == null || request.Input.Length == 0)
            {
                return Index();
            }

            Response.Cookies.Append("searchedWord", request.Input); // add cookie

            var splitInput = request.Input.Split(" "); 
            var resultList = new AnagramViewModel{ WordList = new List<string>() };
            foreach (string word in splitInput) //if word is in cache, add results from cache
            {
                var splitWord = word.Split(" "); // put the word into an array so it could b passed to getAnagrams
                var cacheList = anagramCache.SearchCacheForAnagrams(word);

                if (cacheList.Count == 0) // if word isn't cached use get anagrams and cache
                {
                    var anagramList = new AnagramViewModel { WordList = anagramSolver.GetAnagrams(splitWord) };  
                    CacheWords(anagramList.WordList, word);
                    foreach(string anagramWord in anagramList.WordList)
                    {
                        resultList.WordList.Add(anagramWord);
                    }
                    anagramList.WordList.Clear();
                }
                else
                {
                    foreach(string cacheWord in cacheList)
                    {
                        resultList.WordList.Add(cacheWord);
                    }
                }
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

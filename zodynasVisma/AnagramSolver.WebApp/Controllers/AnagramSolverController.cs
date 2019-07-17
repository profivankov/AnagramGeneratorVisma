using AnagramSolver.Contracts;
using AnagramSolver.EF.DatabaseFirst.Repositories;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace AnagramSolver.WebApp.Controllers
{
    public class AnagramSolverController : Controller
    {
        private readonly IAnagramSolver anagramSolver;
        private ICacheRepository anagramCache;
        private IUserLogRepository userLogRepository;

        public AnagramSolverController(IAnagramSolver anagramSolver, ICacheRepository anagramCache, IUserLogRepository userLogRepository)
        {
            this.anagramSolver = anagramSolver;
            this.anagramCache = anagramCache;
            this.userLogRepository = userLogRepository;
        }

        public IActionResult Index() 
        {
            return View(new AnagramViewModel { WordList = new List<string>() });
        }

        [Route("AnagramSolver/Index/")]
        public IActionResult Index(AnagramViewModel request)  
        {
            if (request.Input == null || request.Input.Length == 0)
            {
                return Index();
            }
            Response.Cookies.Append("searchedWord", request.Input); // add cookie
            var userIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            var resultList = GetWords(request.Input);
            userLogRepository.StoreUserInfo(userIpAddress, request.Input); //new AnagramSolver.Models.UserLogModelCF { SearchedWordID = anagramCache.SearchedWordID(request.Input), IPAdress = userIpAddress, AnagramWord = null, SearchTime = DateTime.Now

            return View(resultList);
        }


        private AnagramViewModel GetWords(string request) // find anagram ID's in DB and put them in a different table
        {
            var splitInput = request.Split(" ");
            var resultList = new AnagramViewModel { WordList = new List<string>() };
            foreach (string word in splitInput) //if word is in cache, add results from cache
            {
                var splitWord = word.Split(" "); // put the word into an array so it could b passed to getAnagrams
                var cacheList = anagramCache.SearchCacheForAnagrams(word);

                if (cacheList.Count == 0) // if word isn't cached use get anagrams and cache
                {
                    var anagramList = new AnagramViewModel { WordList = anagramSolver.GetAnagrams(splitInput) };
                    anagramCache.AddCacheToRepository(anagramList.WordList, word);
                    foreach (string anagramWord in anagramList.WordList)
                    {
                        resultList.WordList.Add(anagramWord);
                    }
                    anagramList.WordList.Clear();
                }
                else
                {
                    foreach (string cacheWord in cacheList)
                    {
                        resultList.WordList.Add(cacheWord);
                    }
                }
            }
            return resultList;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

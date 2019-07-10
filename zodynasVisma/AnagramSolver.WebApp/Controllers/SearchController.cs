using System;
using System.Collections.Generic;
using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

namespace AnagramSolver.WebApp.Controllers
{
    public class SearchController : Controller
    {
        private IWordRepository _wordRepository;

        public SearchController(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public IActionResult Index()
        {
            return View(new SearchViewModel { WordList = new List<string>() });
        }

        public IActionResult Search(SearchViewModel request)
        {
            if(string.IsNullOrEmpty(request.Input))
            {
                return Index();
            }
            var resultList = new SearchViewModel { WordList = _wordRepository.SearchRepository(request.Input) };
            return View(resultList);
        }
    }
}
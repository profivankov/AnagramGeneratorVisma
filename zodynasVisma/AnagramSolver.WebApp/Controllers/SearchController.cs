using System.Collections.Generic;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using AnagramSolver.EF.CodeFirst.Contracts;

namespace AnagramSolver.WebApp.Controllers
{
    public class SearchController : Controller
    {
        private IWordService _wordService;

        public SearchController(IWordService wordService)
        {
            _wordService = wordService;
        }

        public IActionResult Index()
        {
            return View(new SearchViewModel { WordList = new List<string>() });
        }
        [Route("Search/Index/")]
        public IActionResult Index(SearchViewModel request)
        {
            if(string.IsNullOrEmpty(request.Input))
            {
                return Index();
            }
            var resultList = new SearchViewModel { WordList = _wordService.SearchRepository(request.Input) };
            return View(resultList);
        }
    }
}
using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;
using AnagramSolver.EF.CodeFirst.Contracts;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

namespace AnagramSolver.WebApp.Controllers
{
    public class DictionaryController : Controller
    {
        private IWordService _wordService;
        private IUserInfoService _userInfoService;

        public DictionaryController(IWordService wordService, IUserInfoService userInfoService)
        {
            _wordService = wordService;
            _userInfoService = userInfoService;
        }

        public IActionResult Index(int pageNum)
        {
            var list = _wordService.GetDictionary(pageNum).Values.SelectMany(x => x); //.ToList();

            var pager = new Pager(pageNum, _wordService.TotalItems());

            var viewModel = new DictionaryViewModel
            {
                Items = list, //.Skip((pageNum - 1) * 100).Take(pager.PageSize),
                Pager = pager,
            };

            return View(viewModel);
        }

        public FileResult Download()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(@"C:\Users\mantrimas\source\repos\zodynasVisma\zodynasVisma\zodynas.txt");
            string fileName = "dictionary.txt";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }


        public IActionResult AddWord(WordViewModel request)
        {
            if (string.IsNullOrEmpty(request.Input))
            {
                return Empty();
            }
            Response.Cookies.Append("lastAddedWord", request.Input);
            _userInfoService.AddRemoveSearches((_wordService.HasWordBeenAdded(request.Input)));
            return View(request);
        }

        public IActionResult RemoveWord(WordViewModel request)
        {
            if (string.IsNullOrEmpty(request.Input))
            {
                return Empty();
            }
            Response.Cookies.Append("lastRemovedWord", request.Input);
            _userInfoService.AddRemoveSearches((_wordService.HasWordBeenRemoved(request.Input)));
            return View(request);
        }

        public IActionResult EditWord(WordViewModel request)
        {
            if (string.IsNullOrEmpty(request.Input))
            {
                return Empty();
            }
            Response.Cookies.Append("lastEditedWord", request.Input);
            //_userInfoService.AddRemoveSearches(_wordRepository.EditWord(request.Input, request.EditWord));

            return View(request);
        }

        public IActionResult Empty()
        {
            return View(new WordViewModel());
        }
    }
}
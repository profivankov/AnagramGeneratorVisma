using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

namespace AnagramSolver.WebApp.Controllers
{
    public class DictionaryController : Controller
    {
        private IWordRepository _wordRepository;
        private IUserInfoRepository _userInfoRepository;

        public DictionaryController(IWordRepository wordRepository, IUserInfoRepository userInfoRepository)
        {
            _wordRepository = wordRepository;
            _userInfoRepository = userInfoRepository;
        }

        public IActionResult Index(int pageNum)
        {
            var list = _wordRepository.GetDictionary(pageNum).Values.SelectMany(x => x); //.ToList();

            var pager = new Pager(pageNum, _wordRepository.GetTotalItems());

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
            _userInfoRepository.AddRemoveSearches((_wordRepository.AddWord(request.Input)));
            return View(request);
        }

        public IActionResult RemoveWord(WordViewModel request)
        {
            if (string.IsNullOrEmpty(request.Input))
            {
                return Empty();
            }
            Response.Cookies.Append("lastRemovedWord", request.Input);
            _userInfoRepository.AddRemoveSearches((_wordRepository.RemoveWord(request.Input)));
            return View(request);
        }

        public IActionResult EditWord(WordViewModel request)
        {
            if (string.IsNullOrEmpty(request.Input))
            {
                return Empty();
            }
            Response.Cookies.Append("lastEditedWord", request.Input);
            _userInfoRepository.AddRemoveSearches(_wordRepository.EditWord(request.Input, request.EditWord));

            return View(request);
        }

        public IActionResult Empty()
        {
            return View(new WordViewModel());
        }
    }
}
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

        public DictionaryController(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository; 
        }

        public IActionResult Index(int pageNum)
        {
            var list = _wordRepository.GetDictionary(pageNum).Values.SelectMany(x => x); //.ToList();

            var pager = new Pager(pageNum);

            var viewModel = new DictionaryViewModel
            {
                Items = list, //.Skip((pageNum - 1) * 100).Take(pager.PageSize),
                Pager = pager
            };

            return View(viewModel);
        }

        public FileResult Download()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(@"C:\Users\mantrimas\source\repos\zodynasVisma\zodynasVisma\zodynas.txt");
            string fileName = "dictionary.txt";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}
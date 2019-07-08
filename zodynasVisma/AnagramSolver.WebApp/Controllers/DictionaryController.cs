using AnagramSolver.BusinessLogic;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

namespace AnagramSolver.WebApp.Controllers
{
    public class DictionaryController : Controller
    {
        public FileWordRepository dictonaryObject { get; set; }
        StreamReader file;

        public DictionaryController()
        {
            file = new StreamReader(@"C:\Users\mantrimas\source\repos\zodynasVisma\zodynasVisma\zodynas.txt");
        }

        public IActionResult Dictionary(int pageNum)
        {
            dictonaryObject = new FileWordRepository();
            var list = dictonaryObject.GetDictionary(file).Values.SelectMany(x => x); //.ToList();

            var pager = new Pager(list.Count(), pageNum);

            var viewModel = new DictionaryViewModel
            {
                Items = list.Skip((pageNum - 1) * 100).Take(pager.PageSize),
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
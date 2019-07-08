using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.BusinessLogic;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AnagramSolver.WebApp.ApiControllers
{
    [Route("api/anagrams")]
    [ApiController]
    public class ApiController : Controller
    {

        public BusinessLogic.AnagramSolver anagramObject { get; set; }
        StreamReader file;

        public ApiController()
        {
            file = new StreamReader(@"C:\Users\mantrimas\source\repos\zodynasVisma\zodynasVisma\zodynas.txt");
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{input}")]
        public IList<string> Get(string input)
        {

            var splitInput = input.Split(" "); // put the strings seperated by spaces into an array so they can be passed to the GetAnagrams function // need to find a better workaround

            bool success = int.TryParse(ConfigurationManager.AppSettings["MaxResultAmount"], out var _defaultValue);
            if (!success)
            {
                _defaultValue = 100;
            }
            anagramObject = new BusinessLogic.AnagramSolver(new FileWordRepository(), _defaultValue);
            var resultList = new AnagramViewModel { WordList = anagramObject.GetAnagrams(splitInput, file) };
            return resultList.WordList;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

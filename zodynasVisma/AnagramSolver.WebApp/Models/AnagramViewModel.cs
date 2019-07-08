using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;



namespace AnagramSolver.WebApp.Models
{
    public class AnagramViewModel
    {
        public IList<string> wordList { get; set; }

        public string[] input { get; set; }

    }
}

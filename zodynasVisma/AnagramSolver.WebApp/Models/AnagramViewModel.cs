using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;



namespace AnagramSolver.WebApp.Models
{
    public class AnagramViewModel
    {
        public IList<string> WordList { get; set; }

        public string[] Input { get; set; }

    }
}

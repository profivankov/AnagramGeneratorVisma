using System.Collections.Generic;

namespace AnagramSolver.WebApp.Models
{
    public class AnagramViewModel
    {
        public IList<string> WordList { get; set; }
        public string[] Input { get; set; }
    }
}

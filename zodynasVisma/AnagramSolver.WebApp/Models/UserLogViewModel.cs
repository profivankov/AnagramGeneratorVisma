using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramSolver.WebApp.Models
{
    public class UserLogViewModel
    {
        public string IPAdress { get; set; }
        public DateTime SearchTime { get; set; }
        public string SearchedWord { get; set; }
        public List<string> AnagramList { get; set; }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace AnagramSolver.Models
{
    public class UserLogModelCF
    {
        public string IPAdress { get; set; }
        public DateTime SearchTime { get; set; }
        public int SearchedWordID { get; set; }
        public List<string> AnagramWord { get; set; }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace AnagramSolver.Models
{
    public class UserLogListModel
    {
        public List<string> IPAdress { get; set; }
        public List<DateTime> SearchTime { get; set; }
        public List<string> SearchedWord { get; set; }
        public List<string> AnagramList { get; set; }
    }
}

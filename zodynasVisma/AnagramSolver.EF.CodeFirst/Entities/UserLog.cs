using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.CodeFirst.Entities
{
    public partial class UserLog
    {
        public int LogID { get; set; }
        public string UserIP { get; set; }
        public int SearchedWordID { get; set; }
        public DateTime SearchTime { get; set; }

        public SearchedWords SearchedWords { get; set; }
    }
}

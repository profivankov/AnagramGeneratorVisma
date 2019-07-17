using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.EF.CodeFirst.Entities
{
    public class SearchedWords
    {
        public int SearchedWordId { get; set; }
        public string SearchedWord { get; set; }

        public ICollection<CachedWords> CachedWords { get; set; }
        public ICollection<UserLog> UserLog { get; set; }
    }
}

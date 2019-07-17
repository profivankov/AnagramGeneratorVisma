using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.CodeFirst.Entities
{
    public class CachedWords
    {
        public int CacheID { get; set; }

        public int SearchedWordID { get; set; }
        public int? WordID { get; set; }

        public Words Words { get; set; }
        public SearchedWords SearchedWords { get; set; }
    }
}

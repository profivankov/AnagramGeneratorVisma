using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.DatabaseFirst.Models
{
    public partial class CachedWords
    {
        public int CacheId { get; set; }
        public int? AnagramWordId { get; set; }
        public string SearchedWord { get; set; }

        public Words AnagramWord { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.CodeFirst.Entities
{
    public partial class Words
    {
        public int WordID { get; set; }
        public string Word { get; set; }

        public ICollection<CachedWords> CachedWords { get; set; }
    }
}

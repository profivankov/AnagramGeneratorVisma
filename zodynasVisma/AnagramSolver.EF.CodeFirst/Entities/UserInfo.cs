using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.EF.CodeFirst.Entities
{
    public class UserInfo
    {
        public string UserIP { get; set; }
        public int SearchesLeft { get; set; }
        public int TotalSearches { get; set; }

        public ICollection<UserLog> UserLog { get; set; }
    }
}

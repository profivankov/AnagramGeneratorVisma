using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.DatabaseFirst.Models
{
    public partial class UserLog
    {
        public int Id { get; set; }
        public string UserIp { get; set; }
        public string UserSearchedWord { get; set; }
        public DateTime SearchTime { get; set; }
    }
}

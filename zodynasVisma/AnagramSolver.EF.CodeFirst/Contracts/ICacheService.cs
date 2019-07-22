using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.EF.CodeFirst.Contracts
{
    public interface ICacheService
    {
        List<string> GetAnagramsFromCache(string input);
        List<string> GetMultiple(string input);
    }
}

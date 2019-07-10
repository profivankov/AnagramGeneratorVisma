using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Contracts
{
    public interface ICacheRepository
    {
        void AddCacheToRepository(IList<string> list, string word);
        List<string> SearchCacheForAnagrams(string input);
    }
}

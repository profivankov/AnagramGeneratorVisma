using System;
using System.Collections.Generic;
using System.Text;
using AnagramSolver.EF.CodeFirst.Entities;

namespace AnagramSolver.EF.CodeFirst.Contracts
{
    public interface ICacheRepository
    {
        List<string> SearchCacheForAnagrams(string input);

        int GetSearchedWordID(string word);
        int AddSearchedWordID(SearchedWords searchedWords);
        void AddCachedWords(CachedWords cachedWords);
        List<int> GetAnagramIDs(IList<string> list);
    }
}

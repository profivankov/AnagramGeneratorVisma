using System.Collections.Generic;
using System.Linq;
using AnagramSolver.EF.CodeFirst.Contracts;
using AnagramSolver.EF.CodeFirst.Entities;

namespace AnagramSolver.EF.CodeFirst.Repositories
{
    public class EFCFCacheRepository : ICacheRepository
    {
        private DictionaryContext _dbContext;
        public EFCFCacheRepository(DictionaryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddCachedWords(CachedWords cachedWords)
        {
            _dbContext.CachedWords.Add(cachedWords);
            _dbContext.SaveChanges();
        }

        public int AddSearchedWordID(SearchedWords searchedWords)
        {
                _dbContext.SearchedWords.Add(searchedWords);
                _dbContext.SaveChanges();
                return searchedWords.SearchedWordId;
        }

        public int GetSearchedWordID(string word)
        {
            var wordID = _dbContext.SearchedWords.Where(x => x.SearchedWord == word).Select(x => x.SearchedWordId).FirstOrDefault(); // gets searched word ID
            return wordID;
        }

        public int GetWordID(string input)
        {
            return _dbContext.Words.FirstOrDefault(x => x.Word == input).WordID;
        }

        public List<int> GetAnagramIDs(IList<string> list)
        {
           return _dbContext.Words.Where(x => list.Contains(x.Word)).Select(x => x.WordID).ToList();
        }

        public List<string> SearchCacheForAnagrams(string input)
        {
            var anagrams = _dbContext.CachedWords.Where(x => x.SearchedWordID == GetSearchedWordID(input)).Select(x => x.Words.Word).ToList();
            return anagrams;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.Contracts;
using AnagramSolver.EF.CodeFirst;
using AnagramSolver.EF.CodeFirst.Entities;
using AnagramSolver.Models;

namespace AnagramSolver.EF.CodeFirst.Repositories
{
    public class EFCFCacheRepository : ICacheRepository
    {
        private DictionaryContext _dbContext;
        public EFCFCacheRepository(DictionaryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddCacheToRepository(IList<string> list, string word)
        {
            var resultList = new List<int>();
            var searchedWord = SearchedWordID(word);

            if (searchedWord == 0)
            {
                throw new Exception("Zero");
            }

            resultList = _dbContext.Words.Where(x => list.Contains(x.Word)).Select(x => x.WordID).ToList(); // is there anyway to add foreach into this query

            foreach (var item in resultList)
            {
                var cachedWords = new CachedWords()
                {
                    WordID = item,
                    SearchedWordID = searchedWord
                };
                _dbContext.CachedWords.Add(cachedWords);
                _dbContext.SaveChanges();
            }
        }

        public int SearchedWordID(string word) //either gets or adds searched word ID
        {
            var wordID = _dbContext.SearchedWords.Where(x => x.SearchedWord == word).Select(x => x.SearchedWordId).FirstOrDefault(); // gets searched word ID
            if (wordID == 0) // if not returned, add word and ID to table
            {
                var searchedWords = new SearchedWords()
                {
                    SearchedWord = word
                };
                _dbContext.SearchedWords.Add(searchedWords);
                _dbContext.SaveChanges();
                wordID = searchedWords.SearchedWordId;
            }

            return wordID;
        }

        public List<string> SearchCacheForAnagrams(string input)
        {
            // get anagrams  from cahce with searchwordID
            var anagrams = _dbContext.CachedWords.Where(x => x.SearchedWordID == SearchedWordID(input)).Select(x => x.Words.Word).ToList();
            return anagrams;
        }

        public int GetWordID(string input)
        {
            return _dbContext.Words.FirstOrDefault(x => x.Word == input).WordID;
        }


    }
}

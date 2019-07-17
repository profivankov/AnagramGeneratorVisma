using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.Contracts;
using AnagramSolver.EF.CodeFirst;
using AnagramSolver.EF.CodeFirst.Entities;
using AnagramSolver.Models;

namespace AnagramSolver.EF.DatabaseFirst.Repositories
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
            foreach (var entry in list)
            {
                var query = _dbContext.Words.Where(x => x.Word == entry).Select(x => x.WordID).FirstOrDefault(); // is there anyway to add foreach into this query
                resultList.Add(query);
            }


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
                wordID = SearchedWordID(word);
            }

            return wordID;
        }

        public List<string> SearchCacheForAnagrams(string input)
        {
            var resultList = new List<string>();
            var wordID = SearchedWordID(input);
            //var wordID = _dbContext.SearchedWords.Where(x => x.SearchedWord == input).Select(x => x.SearchedWordId).First(); // get SearchWord ID
            // get anagram IDs from cahce with searchwordID
            var anagramIDs = _dbContext.CachedWords.Where(x => x.SearchedWordID == wordID).Select(x => x.WordID).ToList();

            foreach (var ID in anagramIDs) // get word for each ID
            {
                var queryResults = _dbContext.Words.Where(x => x.WordID == ID).Select(x => x.Word).First();
                resultList.Add(queryResults);
            }

            return resultList;
        }

        public int GetWordID(string input)
        {
            return _dbContext.Words.Where(x => x.Word == input).Select(x => x.WordID).FirstOrDefault();
        }


    }
}

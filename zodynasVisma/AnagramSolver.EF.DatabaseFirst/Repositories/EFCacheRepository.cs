using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.Contracts;
using AnagramSolver.EF.DatabaseFirst.Entities;
using AnagramSolver.Models;

namespace AnagramSolver.EF.DatabaseFirst.Repositories
{
    public class EFCacheRepository
    {
        private DictionaryContextDBF _dbContext;
        public EFCacheRepository(DictionaryContextDBF dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddCacheToRepository(IList<string> list, string word)
        {
            var resultList = new List<int>();
            var anagramList = ReadAnagramIDS(list);
            var sortedword = string.Concat(word.OrderBy(c => c));
            for (int i = 0; i < anagramList.IDList.Count(); i++)
            {
                if (string.Concat(anagramList.WordList[i].OrderBy(c => c)) == sortedword)
                {
                    resultList.Add(anagramList.IDList[i]);
                }
            }

            foreach (var item in resultList)
            {
                var cachedWords = new CachedWords()
                {
                    AnagramWordId = item,
                    SearchedWord = word,
                };
                _dbContext.CachedWords.Add(cachedWords);
                _dbContext.SaveChanges();
            }
        }

        public List<string> SearchCacheForAnagrams(string input)
        {
            var resultList = new List<List<string>>();

            var query = from words in _dbContext.CachedWords
                        where words.SearchedWord == input
                        select words.AnagramWordId;

            foreach (var ID in query.ToList<int?>())
            {
                var queryResults = from words in _dbContext.Words
                                   where words.Id == ID
                                   select words.Word;

                resultList.Add(queryResults.ToList());
            }
            var returnList = resultList.SelectMany(d => d).ToList();

            return returnList;
        }

        public CacheModel ReadAnagramIDS(IList<string> receivedList)
        {
            var distinctList = receivedList.Distinct();
            var resultLists = new CacheModel { IDList = new List<int>(), WordList = new List<string>() };
            foreach (var word in distinctList)
            {
                var queryResults = from words in _dbContext.Words
                                   where words.Word == word
                                   select words.Id;
                foreach (var item in queryResults.ToList<int>())  // cj nereik list cuz each words has only 1 ID
                {
                    resultLists.IDList.Add(item);
                    resultLists.WordList.Add(word);
                }
            }
            return resultLists;
        }
    }


}

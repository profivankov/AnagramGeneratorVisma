using AnagramSolver.Contracts;
using AnagramSolver.EF.CodeFirst.Contracts;
using AnagramSolver.EF.CodeFirst.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramSolver.WebApp.Services
{
    public class CacheService : ICacheService
    {
        private readonly IWordRepository _wordRepository;
        private ICacheRepository _anagramCache;
        private IAnagramSolverService _anagramSolverService;

        public CacheService(IAnagramSolverService anagramSolverService, ICacheRepository anagramCache)
        {
            _anagramSolverService = anagramSolverService;
            _anagramCache = anagramCache;
        }

        public List<string> GetMultiple(string input)
        {
            var anagramList = new List<string>();
            foreach (var word in input.Split(" "))
            {
                anagramList = anagramList.Concat(GetAnagramsFromCache(word)).ToList();
            }
            return anagramList;
        }


        public List<string> GetAnagramsFromCache(string input) // returns anagrams from cache or db
        {
            var resultList = new List<string>();

                var cacheList = _anagramCache.SearchCacheForAnagrams(input);

                if (cacheList.Count == 0) // if word isn't cached use get anagrams and cache
                {
                    var anagramList = _anagramSolverService.GetAnagrams(input);
                    AddCacheToRepository(anagramList, input);
                    foreach (string anagramWord in anagramList)
                    {
                        resultList.Add(anagramWord);
                    }
                    anagramList.Clear();
                }
                else
                {
                    foreach (string cacheWord in cacheList)
                    {
                        resultList.Add(cacheWord);
                    }
                }

            return resultList;
        }

        public void AddCacheToRepository(IList<string> list, string word)
        {
            var resultList = _anagramCache.GetAnagramIDs(list);
            if (resultList.Count == 0) //adds to searched word list so userlog can display even if it's not in the dictionary
            {
                SearchedWordID(word);
            }
            foreach (var item in resultList)
            {
                var cachedWords = new CachedWords()
                {
                    WordID = item,
                    SearchedWordID = SearchedWordID(word)
                };
                _anagramCache.AddCachedWords(cachedWords);
            }
        }

        public int SearchedWordID(string word) //either gets or adds searched word ID
        {
            var wordID = _anagramCache.GetSearchedWordID(word); // gets searched word ID
            if (wordID == 0) // if not returned, add word and ID to table
            {
                var searchedWords = new SearchedWords()
                {
                    SearchedWord = word
                };
                wordID = _anagramCache.AddSearchedWordID(searchedWords);
            }
            return wordID;
        }


    }
}

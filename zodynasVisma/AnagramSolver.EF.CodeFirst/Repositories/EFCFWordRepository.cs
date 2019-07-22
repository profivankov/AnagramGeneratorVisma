using AnagramSolver.Contracts;
using AnagramSolver.EF.CodeFirst;
using AnagramSolver.EF.CodeFirst.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AnagramSolver.EF.DatabaseFirst.Repositories
{
    public class EFCFWordRepository : IWordRepository
    {
        private DictionaryContext _dbContext;

        public EFCFWordRepository(DictionaryContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Dictionary<string, List<string>> GetDictionary(int? pageNum)
        {
            Dictionary<string, List<string>> wordList = new Dictionary<string, List<string>>();
            var query = new List<string>();
            if (pageNum == null)
            {
                query = _dbContext.Words.Select(w => w.Word).ToList();
            }
            else
            {
                query = _dbContext.Words.Select(w => w.Word).Skip((int)pageNum * 100).Take(100).ToList();
            }
                var temp = "";
                foreach (var word in query)
                {
                    var sortedWord = string.Concat(word.ToLower().OrderBy(c => c));
                    if (sortedWord != temp)
                    {
                        temp = sortedWord.ToLower();
                        if (!wordList.ContainsKey(temp))
                        {
                            wordList.Add(temp, new List<string>());
                            wordList[temp].Add(word);
                        }
                        else
                        {
                            wordList[temp].Add(word);
                        }
                    }
                    else
                    {
                        wordList[temp].Add(word);
                    }
                }

                return wordList;
        }

        public List<string> SearchRepository(string input)
        {
            var query = _dbContext.Words.Select(w => w.Word).Where(w => w.Contains(input)).ToList();
            return query;
        }

        public int GetTotalItems()
        {
            return _dbContext.Words.Count();
        }

        public bool AddWord(string input)
        {
            var words = new Words()
            {
                Word = input
            };
            _dbContext.Words.Add(words);
            _dbContext.SaveChanges();

            return true;
        }

        public bool RemoveWord(string input)
        {
            var words = new Words { Word = input, WordID = _dbContext.Words.Where(x => x.Word == input).Select(x => x.WordID).First() };
            _dbContext.Remove(words);
            _dbContext.SaveChanges();

            return false;
        }

        public bool EditWord(string wordToEdit, string newWord)
        {
            // update word
            var words = new Words
            {
                Word = newWord,
                WordID = _dbContext.Words.Where(x => x.Word == wordToEdit).Select(x => x.WordID).First()
            };
            _dbContext.Words.Update(words);
            _dbContext.SaveChanges();

            // delete entries in cached words
            //select searchwordIDS associated with word
            var searchWordIDs = _dbContext.CachedWords.Where(x => x.Words.Word == wordToEdit).Select(x => x.SearchedWordID);

            //for (int i=0; i<_dbContext.CachedWords.Where(SearchedWord))
            foreach (var ID in searchWordIDs)
            {
                var cache = _dbContext.CachedWords.Where(x => x.SearchedWords.SearchedWordId == ID)
                    .Select(x => new CachedWords
                    {
                        CacheID = x.CacheID,
                        SearchedWordID = x.SearchedWordID,
                        WordID = x.WordID
                    }).ToList();
                _dbContext.Remove(cache);
                _dbContext.SaveChanges();
            }
            //var cacheID = _dbContext.CachedWords.Where(x => x.SearchedWordID == searchedWordID).Select(x => x.CacheID);

            //foreach (var ID in cacheID)
            //{
            //    var cache = new CachedWords {
            //        CacheID = ID,
            //        SearchedWordID = ID,
            //        WordID = _dbContext.CachedWords.Where(x => x.CacheID == ID).Select(x => x.WordID).First(),
            //    };
            //    _dbContext.Remove(cache);
            //    _dbContext.SaveChanges();
            //}

           

            

            return true;
        }
    }
}

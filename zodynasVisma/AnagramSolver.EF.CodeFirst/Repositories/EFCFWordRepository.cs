using AnagramSolver.EF.CodeFirst;
using AnagramSolver.EF.CodeFirst.Contracts;
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
        public List<string> GetAllWords()
        {
            return _dbContext.Words.Select(w => w.Word).ToList();
        }

        public List<string> GetSkipWords(int? pageNum)
        {
            return _dbContext.Words.Select(w => w.Word).Skip((int)pageNum * 100).Take(100).ToList();
        }

        public List<string> GetSearchResults(string input)
        {
            return _dbContext.Words.Select(w => w.Word).Where(w => w.Contains(input)).ToList();
        }

        public int GetTotalItems()
        {
            return _dbContext.Words.Count();
        }

        public int GetWordID(string input)
        {
            return _dbContext.Words.Where(x => x.Word == input).Select(x => x.WordID).First();
        }

        public void AddWord(Words words)
        {
            _dbContext.Words.Add(words);
            _dbContext.SaveChanges();
        }

        public void RemoveWord(Words words)
        {
            _dbContext.Remove(words);
            _dbContext.SaveChanges();
        }


        //public bool EditWord(string wordToEdit, string newWord)
        //{
        //    // update word
        //    var words = new Words
        //    {
        //        Word = newWord,
        //        WordID = _dbContext.Words.Where(x => x.Word == wordToEdit).Select(x => x.WordID).First()
        //    };
        //    _dbContext.Words.Update(words);
        //    _dbContext.SaveChanges();

        //    // delete entries in cached words
        //    //select searchwordIDS associated with word
        //    var searchWordIDs = _dbContext.CachedWords.Where(x => x.Words.Word == wordToEdit).Select(x => x.SearchedWordID);

        //    //for (int i=0; i<_dbContext.CachedWords.Where(SearchedWord))
        //    foreach (var ID in searchWordIDs)
        //    {
        //        var cache = _dbContext.CachedWords.Where(x => x.SearchedWords.SearchedWordId == ID)
        //            .Select(x => new CachedWords
        //            {
        //                CacheID = x.CacheID,
        //                SearchedWordID = x.SearchedWordID,
        //                WordID = x.WordID
        //            }).ToList();
        //        _dbContext.Remove(cache);
        //        _dbContext.SaveChanges();
        //    }
        //    //var cacheID = _dbContext.CachedWords.Where(x => x.SearchedWordID == searchedWordID).Select(x => x.CacheID);

        //    //foreach (var ID in cacheID)
        //    //{
        //    //    var cache = new CachedWords {
        //    //        CacheID = ID,
        //    //        SearchedWordID = ID,
        //    //        WordID = _dbContext.CachedWords.Where(x => x.CacheID == ID).Select(x => x.WordID).First(),
        //    //    };
        //    //    _dbContext.Remove(cache);
        //    //    _dbContext.SaveChanges();
        //    //}





        //    return true;
        //}
    }
}

using AnagramSolver.Contracts;
using AnagramSolver.EF.CodeFirst;
using AnagramSolver.EF.CodeFirst.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AnagramSolver.EF.CodeFirst.Repositories
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
                    var sortedWord = string.Concat(word.OrderBy(c => c));
                    if (sortedWord != temp)
                    {
                        temp = sortedWord;
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
    }
}

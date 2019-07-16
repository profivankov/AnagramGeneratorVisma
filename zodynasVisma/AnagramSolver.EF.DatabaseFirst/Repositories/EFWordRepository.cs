using AnagramSolver.Contracts;
using AnagramSolver.EF.DatabaseFirst.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramSolver.EF.DatabaseFirst.Repositories
{
    public class EFWordRepository : IWordRepository
    {
        private DictionaryContext _dbContext;
        private Dictionary<string, List<string>> _dictionary;

        public EFWordRepository(DictionaryContext dbContext)
        {
            _dbContext = dbContext;
            //_dictionary = GetDictionary();
        }
        public Dictionary<string, List<string>> GetDictionary(int pageNum)
        {
            Dictionary<string, List<string>> wordList = new Dictionary<string, List<string>>();
                var query = _dbContext.Words.Select(w => w.Word).Skip(pageNum * 100).Take(100).ToList();
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

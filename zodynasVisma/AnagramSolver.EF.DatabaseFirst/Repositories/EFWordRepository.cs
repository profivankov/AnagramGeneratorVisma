using AnagramSolver.Contracts;
using AnagramSolver.EF.DatabaseFirst.Models;
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

        public EFWordRepository(DictionaryContext context)
        {
            _dbContext = context;
            _dictionary = GetDictionary();
        }
        public Dictionary<string, List<string>> GetDictionary()
        {
            Dictionary<string, List<string>> wordList = new Dictionary<string, List<string>>();
            if (_dictionary == null)
            {
                var query = _dbContext.Words.Select(w => w.Word).ToList();
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
            else
            {
                return _dictionary;
            }
        }
        public List<string> SearchRepository(string input)
        {
            var query = _dbContext.Words.Select(w => w.Word).Where(w => w.Contains(input)).ToList();
            return query;
        }
    }
}

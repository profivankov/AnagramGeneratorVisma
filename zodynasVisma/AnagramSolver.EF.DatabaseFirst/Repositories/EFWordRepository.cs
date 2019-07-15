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
        private DictionaryContext db;
        private Dictionary<string, List<string>> Dict;

        public EFWordRepository()
        {
            db = new DictionaryContext();
            Dict = GetDictionary();
        }
        public Dictionary<string, List<string>> GetDictionary()
        {
            Dictionary<string, List<string>> wordList = new Dictionary<string, List<string>>();
            if (Dict == null)
            {
                var query = db.Words.Select(w => w.Word).ToList();
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
                return Dict;
            }
        }
        public List<string> SearchRepository(string input)
        {
            var query = db.Words.Select(w => w.Word).Where(w => w.Contains(input)).ToList();
            return query;
        }
    }
}

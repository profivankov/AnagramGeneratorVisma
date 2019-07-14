using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.Contracts;
using AnagramSolver.EF.DatabaseFirst.Models;

namespace AnagramSolver.EF.DatabaseFirst.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private DictionaryContext db;
        public CacheRepository()
        {
            db = new DictionaryContext();
        }

        public void AddCacheToRepository(IList<string> list, string word)
        {
            throw new NotImplementedException();
        }

        public List<string> SearchCacheForAnagrams(string input)
        {
            throw new NotImplementedException();
        }

        public void ReadAnagramIDS()
        {
            var q = from words in db.Words
                    where words.Word == "labas"
                    select words.Id;

        }
    }


}

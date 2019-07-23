using AnagramSolver.EF.CodeFirst.Contracts;
using AnagramSolver.EF.CodeFirst.Entities;
using System.Collections.Generic;
using System.Linq;

namespace AnagramSolver.WebApp.Services
{
    public class WordService : IWordService
    {
        private IWordRepository _wordRepository;
        public WordService(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public Dictionary<string, List<string>> GetDictionary(int? pageNum)
        {
            Dictionary<string, List<string>> wordList = new Dictionary<string, List<string>>();
            var query = new List<string>();
            if (pageNum == null)
            {
                query = _wordRepository.GetAllWords();
            }
            else
            {
                query = _wordRepository.GetSkipWords(pageNum);
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
            return _wordRepository.GetSearchResults(input);
        }

        public int TotalItems()
        {
            return _wordRepository.GetTotalItems();
        }

        public bool HasWordBeenAdded(string input)
        {
            var words = new Words()
            {
                Word = input
            };
            _wordRepository.AddWord(words);
            return true;
        }

        public bool HasWordBeenRemoved(string input)
        {
            var words = new Words { Word = input, WordID = _wordRepository.GetWordID(input) };
            _wordRepository.RemoveWord(words);
            return false;
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using AnagramSolver.Contracts;
using System.IO;

namespace AnagramSolver.BusinessLogic
{
    public class AnagramSolver : IAnagramSolver
    {
        private IWordRepository _wordRepository;
        public int _maxResultAmount;
        public List<string> finalList;

        public AnagramSolver(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
            finalList = new List<string>();
        }

        public int MaxResultAmount()
        {
            bool success = int.TryParse(ConfigurationManager.AppSettings["MaxResultAmount"], out var _maxResultAmount);
            if (!success)
            {
                _maxResultAmount = 100;
            }
            return _maxResultAmount;
        }

        public IList<string> GetAnagrams(string[] input) 
        {
            var counter = 0;
            _maxResultAmount = MaxResultAmount();
            var wordList = _wordRepository.GetDictionary(null);

            foreach (string splitWord in input)
            {
                var currentWord = string.Concat(splitWord.ToLower().OrderBy(c => c));

                if (wordList.ContainsKey(currentWord))
                {
                    foreach (string s in wordList[currentWord])
                    {
                        finalList.Add(s); // adds every anagram from the list
                    }
                    counter += wordList[currentWord].Count(); // counting how many words being output
                }
                else
                {
                    Console.WriteLine("No such word(s) found.");
                    return finalList;
                }
                if (counter >= _maxResultAmount)
                {
                    Console.WriteLine("Maximum result amount exceeded");
                    finalList.Add("MAXWORDSREACHED");
                    return finalList;
                }
            }
            return finalList;
        }
    }
}


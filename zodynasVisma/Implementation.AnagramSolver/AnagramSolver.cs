using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using AnagramSolver.Contracts;

namespace AnagramSolver.BusinessLogic
{
    public class AnagramGenerator : IAnagramSolver
    {
        private IWordRepository _wordRepository;
        private int _maxResultAmount;
        private int _defaultValue;
        private bool success;
        public List<string> finalList;

        public AnagramGenerator(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
            success = int.TryParse(ConfigurationManager.AppSettings["MaxResultAmount"], out _defaultValue);
            if (success)
            {
                _maxResultAmount = _defaultValue;
            }
            else
                _maxResultAmount = 100;
            finalList = new List<string>();
        }

        public IList<string> GetAnagrams(string[] input) 
        {
            var counter = 0;
            Dictionary<string, List<string>> wordList = _wordRepository.GetDictionary();

            foreach (string splitWord in input)
            {
                var currentWord = string.Concat(splitWord.OrderBy(c => c));

                if (wordList.ContainsKey(currentWord))
                {
                    finalList.Add(splitWord); //
                    finalList.Add("\n");
                    //wordList[currentWord].ForEach(Console.WriteLine);
                    foreach (string s in wordList[currentWord])
                    {
                        finalList.Add(s); // adds every anagram from the list
                    }
                    counter += wordList[currentWord].Count(); // counting how many words being output
                    finalList.Add("___________________\n");
                }
                else
                {
                    Console.WriteLine("No such word(s) found.");
                    return finalList;
                }
                if (counter > _maxResultAmount)
                {
                    Console.WriteLine("Maximum result amount exceeded");
                    return finalList;
                }
            }
            return finalList;
        }
    }
}


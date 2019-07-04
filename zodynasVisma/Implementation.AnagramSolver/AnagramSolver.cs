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
        private int _maxResultAmount;
        public List<string> finalList;

        public AnagramSolver(IWordRepository wordRepository, int maxResultAmount)
        {
            _wordRepository = wordRepository;
            finalList = new List<string>();
            _maxResultAmount = maxResultAmount;
        }

        public IList<string> GetAnagrams(string[] input, StreamReader file) 
        {
            var counter = 0;
            Dictionary<string, List<string>> wordList = _wordRepository.GetDictionary(file);

            foreach (string splitWord in input)
            {
                var currentWord = string.Concat(splitWord.OrderBy(c => c));

                if (wordList.ContainsKey(currentWord))
                {
                    //finalList.Add(splitWord); //
                    //finalList.Add("\n");
                    //wordList[currentWord].ForEach(Console.WriteLine);
                    foreach (string s in wordList[currentWord])
                    {
                        finalList.Add(s); // adds every anagram from the list
                    }
                    counter += wordList[currentWord].Count(); // counting how many words being output
                    //finalList.Add("___________________\n");
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


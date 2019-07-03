using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Collections;
using Interfaces.AnagramSolver;

namespace Implementation.AnagramSolver
{
    public class AnagramSolver : IAnagramSolver
    {
        private IWordRepository _wordRepository;
        private int _maxResultAmount;
        public List<string> finalList;

        public AnagramSolver(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
            _maxResultAmount = int.Parse(ConfigurationManager.AppSettings["MaxResultAmount"]);
            finalList = new List<string>();
        }

        public IList<string> GetAnagrams(string[] myWords) 
        {
            var input = myWords;
            var counter = 0;
            var i = 0;
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
                }
                if (counter > _maxResultAmount)
                {
                    Console.WriteLine("Maximum result amount exceeded");
                    return null;
                }
            }
            return finalList;
        }
    }
}


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

        public AnagramSolver(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }


        public IList<string> GetAnagrams(string[] myWords) 
        {
            var input = myWords;
            var counter = 0;
            Dictionary<string, List<string>> wordList = _wordRepository.GetDictionary();

            foreach (string splitWord in input)
            {
                var currentWord = string.Concat(splitWord.OrderBy(c => c));

                if (wordList.ContainsKey(currentWord))
                {
                    Console.WriteLine("{0}", splitWord);
                    wordList[currentWord].ForEach(Console.WriteLine);
                    counter += wordList[currentWord].Count(); // counting how many words being output
                    Console.WriteLine("___________________\n");
                }
                else
                {
                    Console.WriteLine("No such word(s) found.");
                }
                if (counter > int.Parse(ConfigurationManager.AppSettings["MaxResultAmount"]))
                {
                    Console.WriteLine("Maximum result amount exceeded");
                    return null;
                }
            }

            return null;
        }
    }
}


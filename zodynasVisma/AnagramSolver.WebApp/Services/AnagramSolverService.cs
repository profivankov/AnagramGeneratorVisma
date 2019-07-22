using AnagramSolver.Contracts;
using AnagramSolver.EF.CodeFirst.Contracts;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

namespace AnagramSolver.WebApp.Services
{
    public class AnagramSolverService : IAnagramSolverService
    {
        private readonly IWordRepository _wordRepository;
        public List<string> finalList;
        public int _maxResultAmount;

        public AnagramSolverService(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
            finalList = new List<string>();
        }

        public IList<string> GetAnagrams(string input)
        {
            var counter = 0;
            _maxResultAmount = MaxResultAmount();
            var wordList = _wordRepository.GetDictionary(null);


            var currentWord = string.Concat(input.ToLower().OrderBy(c => c));

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
            return finalList;
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
    }
}

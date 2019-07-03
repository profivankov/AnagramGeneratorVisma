using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Implementation.AnagramSolver
{
    public class CheckWordLength
    {
        private static int _minWordLength;
        static CheckWordLength() //static constructor
        {
            _minWordLength = int.Parse(ConfigurationManager.AppSettings["MinWordLength"]);
        }

        public static bool checkInput(string[] wordList)
        {
            foreach(string word in wordList)
            {
                if(word.Length < _minWordLength)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

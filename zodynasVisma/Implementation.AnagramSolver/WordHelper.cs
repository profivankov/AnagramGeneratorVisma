using System.Configuration;

namespace AnagramSolver.BusinessLogic
{
    public static class WordHelper
    {
        private static int _minWordLength;
        private static int _defaultValue;
        private static bool success;

        
        static WordHelper() //static constructor
        {
            success = int.TryParse(ConfigurationManager.AppSettings["MinWordLength"], out _defaultValue);
            if (success)
            {
                _minWordLength = _defaultValue;
            }
            else
                _minWordLength = 2;
        }

        public static bool CheckInput(string[] wordList)
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

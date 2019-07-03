using System;
using System.Configuration;
using Implementation.AnagramSolver;
using Interfaces.AnagramSolver;
using System.Collections.Generic;

namespace MainApp
{
    class Program
    {
        public List<string> finalList = new List<string>();
        static void Main(string[] args)
        {
            var minWordLength = ConfigurationManager.AppSettings["MinWordLength"];
            if(!CheckWordLength.checkInput(args))
                throw new Exception(String.Format("One or some of the words are too short")); ; 


            var amtElements = args.GetLength(0);
            if (amtElements > 10 || amtElements <= 0)
                throw new Exception(String.Format("Incorrect word amount")); // negaliu leist useriui iš naujo įrašinėt žodžių nes console blogai nuskaito LT raides 

            string[] myWords = args;
            var object1 = new AnagramSolver(new FileWordRepository());

            foreach (string s in object1.GetAnagrams(myWords))
            {
                Console.WriteLine("{0}", s);
            }
        
        }
    }
}

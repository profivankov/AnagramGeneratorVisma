using System;
using System.Configuration;
using Implementation.AnagramSolver;
using Interfaces.AnagramSolver;

namespace MainApp
{
    class Program
    {

        static void Main(string[] args)
        {
            int minWordLength = ;
            var maxAnagrams = ConfigurationManager.AppSettings["MaxResultAmount"];

            var amtElements = args.GetLength(0);
            if (amtElements > 10 || amtElements <= 0)
                throw new Exception(String.Format("Incorrect word amount")); // negaliu leist useriui iš naujo įrašinėt žodžių nes console blogai nuskaito LT raides 

            string[] myWords = args;
            var object1 = new AnagramSolver(new FileWordRepository());
             object1.GetAnagrams(myWords);
        }
    }
}

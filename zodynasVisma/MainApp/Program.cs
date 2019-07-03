using System;
using System.Configuration;
using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;
using System.Collections.Generic;

namespace AnagramSolver.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            if(!WordHelper.CheckInput(args))
                throw new Exception(String.Format("One or some of the words is too short")); ; 


            var amtElements = args.GetLength(0);
            if (amtElements > 10 || amtElements <= 0)
                throw new Exception(String.Format("Incorrect word amount")); // negaliu leist useriui iš naujo įrašinėt žodžių nes console blogai nuskaito LT raides 

            string[] myWords = args;
            var object1 = new AnagramGenerator(new FileWordRepository());

            foreach (string s in object1.GetAnagrams(myWords))
            {
                System.Console.WriteLine("{0}", s); //yra using system bet doesn't work without it?
            }
        
        }
    }
}

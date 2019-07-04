using System;
using System.Configuration;
using System.IO;
using AnagramSolver.BusinessLogic;


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


            bool success = int.TryParse(ConfigurationManager.AppSettings["MaxResultAmount"], out var _defaultValue);
            if (!success)
            {
                _defaultValue = 100;
            }

            var file = new StreamReader(@"C:\Users\mantrimas\source\repos\zodynasVisma\zodynasVisma\zodynas.txt");

            string[] myWords = args;
            var object1 = new BusinessLogic.AnagramSolver(new FileWordRepository(), _defaultValue);

            foreach (string s in object1.GetAnagrams(myWords, file))
            {
                System.Console.WriteLine("{0}", s); //yra using system bet doesn't work without it?
            }
        
        }
    }
}

using System;
using Implementation.AnagramSolver;

namespace MainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter words:");
            var myWords = Console.ReadLine();
            var object1 = new AnagramSolver();
            object1.GetAnagrams(myWords);
        }
    }
}

using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Implementation.AnagramSolver
{
    public class AnagramSolver
    {
        //static string SortForHash(string word)
        //{
        //    string hashThis = String.Concat(word.OrderBy(c => c)); // sort word for hash 
        //    return hashThis;
        //}

        //static int HashFunction(string s) //hash function  
        //{
        //    var isAlpha = char.IsLetter(s[0]);
        //    bool isAscii = s[0] < 128;
        //    if (isAlpha == true && isAscii == true)
        //    {
        //        return Char.ToLower(s[0]) - 'a';
        //    }
        //    else
        //        return 27;
        //}


        public void FindAnagram()
        {
            var input = Console.ReadLine();

            var dict = new FileWordRepository();
            Dictionary<string, List<string>> wordList = dict.GetDictionary();

            input = String.Concat(input.OrderBy(c => c));

            if (wordList.ContainsKey(input))
            {

                wordList[input].ForEach(Console.WriteLine);
                //foreach (var value in wordList[input].Values)
                //{
                //    value.ForEach(Console.WriteLine);
                //}
                //Console.WriteLine();
            }
                    



            //List<string> sortedList = wordList[i];
            //sortedList.Sort();
            ////sortedList.ForEach(Console.WriteLine);
            ////string result = sortedList.Single(s => s == input);                                                               
            //IEnumerable<string> result = sortedList.Where(s => SortForHash(s) == sortedInput);

            ////List<string> newList = sortedList.Where(obj => (obj = "aa")); // where sorted object with hash = other sorted objec with hash

            //foreach (string s in result)
            //{
            //    Console.WriteLine(s);
            //}
        }
    }
}


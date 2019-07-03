using Interfaces.AnagramSolver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Implementation.AnagramSolver
{
    public class FileWordRepository : IWordRepository
    {
        public Dictionary<string, List<string>> GetDictionary()
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            Dictionary<string, List<string>> wordList = new Dictionary<string, List<string>>();

            var file = new StreamReader(@"C:\Users\mantrimas\source\repos\zodynasVisma\zodynasVisma\test.txt"); // try catch reiktu į main

            var line = "";
            var temp = "";
            var temp2 = "";
            while (( line = file.ReadLine()) != null)
            {

                string[] delimitedByTab = line.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                var sortedDelimitedByTabOne = string.Concat(delimitedByTab[0].OrderBy(c => c));
                var sortedDelimitedByTabTwo = String.Concat(delimitedByTab[2].OrderBy(c => c));


                if (sortedDelimitedByTabOne != temp) 
                {
                    temp = String.Concat(delimitedByTab[0].OrderBy(c => c));

                    if (!wordList.ContainsKey(temp)) // if no such key add key
                    {
                        wordList.Add(temp, new List<string>());
                        wordList[temp].Add(delimitedByTab[0]);
                    }
                    else
                        wordList[temp].Add(delimitedByTab[0]); // add to list

                    // check second entry key and add to list
                    temp2 = String.Concat(delimitedByTab[2].OrderBy(c => c));
                    if (!wordList.ContainsKey(temp2))
                    {
                        wordList.Add(temp2, new List<string>());
                        wordList[temp2].Add(delimitedByTab[2]);
                    }
                    else
                        wordList[temp2].Add(delimitedByTab[2]);
                }
            }
            return wordList;
        }

    }
}

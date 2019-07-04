using AnagramSolver.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AnagramSolver.BusinessLogic
{
    public class FileWordRepository : IWordRepository
    {
        public Dictionary<string, List<string>> GetDictionary(StreamReader file)
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            Dictionary<string, List<string>> wordList = new Dictionary<string, List<string>>();

   
            var line = "";
            var temp = "";
            while (( line = file.ReadLine()) != null)
            {

                string[] delimitedByTab = line.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                var sortedDelimitedByTabOne = string.Concat(delimitedByTab[0].OrderBy(c => c));
                var sortedDelimitedByTabTwo = String.Concat(delimitedByTab[2].OrderBy(c => c));


                if (sortedDelimitedByTabOne != temp) 
                {
                    temp = string.Concat(delimitedByTab[0].OrderBy(c => c));

                    if (!wordList.ContainsKey(temp)) // if no such key add key
                    {
                        wordList.Add(temp, new List<string>());
                        wordList[temp].Add(delimitedByTab[0]);
                    }
                    else
                        wordList[temp].Add(delimitedByTab[0]); // add to list

                    // check second entry key and add to list
                    string currentWord = string.Concat(delimitedByTab[2].OrderBy(c => c));
                    if (!wordList.ContainsKey(currentWord))
                    {
                        wordList.Add(currentWord, new List<string>());
                        wordList[currentWord].Add(delimitedByTab[2]);
                    }
                    else
                        wordList[currentWord].Add(delimitedByTab[2]);
                }
            }
            return wordList;
        }



    }
}

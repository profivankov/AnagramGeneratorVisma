using System;
using System.Collections.Generic;
using System.Text;
using Interfaces.AnagramSolver;

namespace Implementation.AnagramSolver
{
    class FileReader : IWordRepository // 
    {
        public void ReadFromFile() // read dictionary into hashtable
        {
            List<string>[] wordList = new List<string>[28]; // initializing array of lists

            for (int i = 0; i < wordList.Length; i++)
            {
                wordList[i] = new List<string>();
            }


            var file =
                new System.IO.StreamReader(@"C:\Users\mantrimas\source\repos\zodynasVisma\zodynasVisma\test.txt");

            while ((line = file.ReadLine()) != null) // fill the list
            {
                string[] delimitedByTab = line.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);

                string hashThis = SortForHash(delimitedByTab[0]);
                wordList[HashFunction(hashThis)].Add(delimitedByTab[0]); // Adds all the words to different lists to speed up search

                hashThis = SortForHash(delimitedByTab[2]);
                wordList[HashFunction(hashThis)].Add(delimitedByTab[2]);
            }




            //wordList[1].ForEach(Console.WriteLine); // check if list works
            FindAnagram(wordList);
        }
    }
}

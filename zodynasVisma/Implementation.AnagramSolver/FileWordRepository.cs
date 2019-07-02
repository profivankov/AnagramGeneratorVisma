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

            Dictionary<string, List<string>> wordList = new Dictionary<string, List<string>>();

            var file = new StreamReader(@"C:\Users\mantrimas\source\repos\zodynasVisma\zodynasVisma\test.txt");
            var line = "";
            string temp = "";
            string temp2 = "";
            while (( line = file.ReadLine()) != null)
            {

                string[] delimitedByTab = line.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                var sortedDelimitedByTabOne = String.Concat(delimitedByTab[0].OrderBy(c => c));
                var sortedDelimitedByTabTwo = String.Concat(delimitedByTab[2].OrderBy(c => c));


                if (sortedDelimitedByTabOne != temp) 
                {
                    /* temp = delimitedByTab[0].OrderBy(x => x).ToString();*/
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
                //else if (sortedDelimitedByTabOne == temp)
                //{

                //    if (sortedDelimitedByTabTwo != temp)
                //    {
                //        temp = String.Concat(delimitedByTab[2].OrderBy(c => c));
                //        if (!wordList.ContainsKey(temp))
                //        {
                //            wordList.Add(temp, new List<string>()); // if temp is diff from dict. key, add new key and create new list
                //        }
                //        else
                //            wordList[temp].Add(delimitedByTab[2]);
                //    }
                //}
                //else
                //    wordList[temp].Add(delimitedByTab[0]);

                //    {
                //        wordList[delimitedByTab[1]].Add(delimitedByTab[0], delimitedByTab[2]); // store in seperate dictionary kur vardininkas yra key

                //    }
                //    else // jei keiciasi create new dictionary kur vardininkas yra key
                //    {
                //        wordList[delimitedByTab[1]] = new Dictionary<string, List<string>> { delimitedByTab[0], delimitedByTab[2] };
                //    }
                //}
            }

            //return wordList;
            return wordList;
        }


        //public void ReadFromFile() // read dictionary into hashtable
        //{
        //    List<string>[] wordList = new List<string>[28]; // initializing array of lists

        //    for (int i = 0; i < wordList.Length; i++)
        //    {
        //        wordList[i] = new List<string>();
        //    }


        //    var file =
        //        new System.IO.StreamReader(@"C:\Users\mantrimas\source\repos\zodynasVisma\zodynasVisma\test.txt");

        //    while ((line = file.ReadLine()) != null) // fill the list
        //    {
        //        string[] delimitedByTab = line.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);

        //        string hashThis = SortForHash(delimitedByTab[0]);
        //        wordList[HashFunction(hashThis)].Add(delimitedByTab[0]); // Adds all the words to different lists to speed up search

        //        hashThis = SortForHash(delimitedByTab[2]);
        //        wordList[HashFunction(hashThis)].Add(delimitedByTab[2]);
        //    }




        //    //wordList[1].ForEach(Console.WriteLine); // check if list works
        //    FindAnagram(wordList);
        //}

    }
}

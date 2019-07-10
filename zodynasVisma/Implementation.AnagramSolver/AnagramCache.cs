using AnagramSolver.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AnagramSolver.BusinessLogic
{
    public class AnagramCache : ICacheRepository
    {
        public void AddCacheToRepository(IList<string> list, string word)
        {
            using (var connection = new SqlConnection("Server=LT-LIT-SC-0116\\ANAGRAMSOLVER; Database=Dictionary; Integrated Security=true"))
            {
                var resultList = ReadAnagramIDs(list);
                connection.Open();
                var cmd = new SqlCommand("INSERT INTO CachedWords (Anagram_Word_ID, Searched_Word) VALUES (@readID, @SearchedWord)", connection);

                cmd.Parameters.Add(new SqlParameter("@readID", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@SearchedWord", SqlDbType.NVarChar));
                cmd.Parameters[1].Value = word;

                foreach (int readID in resultList)
                {
                    
                    cmd.Parameters[0].Value = readID;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<int> ReadAnagramIDs(IList<string> list)
        {
            var resultList = new List<int>();
            using (var connection = new SqlConnection("Server=LT-LIT-SC-0116\\ANAGRAMSOLVER; Database=Dictionary; Integrated Security=true"))
            {
                connection.Open();

                var cmd = new SqlCommand("SELECT ID FROM Words WHERE Word=@word", connection);
                cmd.Parameters.Add(new SqlParameter("@word", SqlDbType.NVarChar));
                foreach (string s in list)
                {
                    cmd.Parameters["@word"].Value = s;
                    var dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            resultList.Add(dr.GetInt32(0));
                        }
                    }
                    dr.Close();
                }
            }
            return resultList;
        }
        public List<string> SearchCacheForAnagrams(string input)
        {
            // search database for string, return list
            // if empty, return null
            var list = new List<int>();
            var resultList = new List<string>();

            //var wordIDList = ReadAnagramIDs(list);



            //can use ReadAnagramIDS for this I think
            using (var connection = new SqlConnection("Server=LT-LIT-SC-0116\\ANAGRAMSOLVER; Database=Dictionary; Integrated Security=true"))
            {
                connection.Open();
                var cmd = new SqlCommand("SELECT Anagram_Word_ID FROM CachedWords WHERE Searched_Word=@word", connection);
                cmd.Parameters.Add(new SqlParameter("@word", SqlDbType.NVarChar));
                cmd.Parameters["@word"].Value = input;
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        list.Add(dr.GetInt32(0));
                    }
                }
                dr.Close();
            }

            using (var connection = new SqlConnection("Server=LT-LIT-SC-0116\\ANAGRAMSOLVER; Database=Dictionary; Integrated Security=true"))
            {
                connection.Open();
                var cmd = new SqlCommand("SELECT Word FROM Words WHERE ID=@ID", connection);
                cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
                
                foreach (int ID in list)
                {
                    cmd.Parameters["@ID"].Value = ID;
                    var dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            resultList.Add(dr.GetString(0));
                        }
                    }
                    dr.Close();
                }
            }

            return resultList;
        }
    }
}

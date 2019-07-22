using AnagramSolver.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AnagramSolver.Models;


namespace AnagramSolver.BusinessLogic
{
    public class AnagramCache
    {
        private readonly string _connectionString;
        public AnagramCache(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddCacheToRepository(IList<string> list, string word)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var resultList = new List<int>();
                var anagramList = ReadAnagramIDs(list); 
                var sortedword = string.Concat(word.OrderBy(c => c));
                for (int i=0; i<anagramList.IDList.Count(); i++)
                {
                    if (string.Concat(anagramList.WordList[i].OrderBy(c => c)) == sortedword)
                    {
                        resultList.Add(anagramList.IDList[i]);
                    }
                }
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
        private CacheModel ReadAnagramIDs(IList<string> receivedList)
        {
            var resultLists = new CacheModel { IDList = new List<int>(), WordList = new List<string>() };
            var list = receivedList.Distinct();
            //var resultList = new List<int>();
            using (var connection = new SqlConnection(_connectionString))
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
                            resultLists.IDList.Add(dr.GetInt32(0));
                            resultLists.WordList.Add(s);
                        }
                    }
                    dr.Close();
                }
            }
            return resultLists;
        }
        public List<string> SearchCacheForAnagrams(string input)
        {
            // search database for string, return list
            // if empty, return null
            var list = new List<int>();
            var resultList = new List<string>();
            //can use ReadAnagramIDS for this I think
            using (var connection = new SqlConnection(_connectionString))
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

            using (var connection = new SqlConnection(_connectionString))
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

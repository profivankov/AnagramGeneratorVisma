using AnagramSolver.Contracts;
using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace AnagramSolver.BusinessLogic
{
    public class SQLWordRepository : IWordRepository
    {
        private Dictionary<string, List<string>> Dict;

        private readonly string _connectionString;
        

        public SQLWordRepository(string connectionString)
        {
            _connectionString = connectionString; //ConfigurationManager.AppSettings["connectionString"];
           // Dict = GetDictionary(); figure out how to skip here
        }
        public Dictionary<string, List<string>> GetDictionary(int? pageNum)
        {
            Dictionary<string, List<string>> wordList = new Dictionary<string, List<string>>();
            if (Dict == null)
            { 
            Console.OutputEncoding = Encoding.UTF8;
            
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM Words";
                    var dr = cmd.ExecuteReader();
                    var temp = "";

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var readWord = dr.GetString(1);
                            var sortedWord = string.Concat(readWord.OrderBy(c => c));
                            if (sortedWord != temp)
                            {
                                temp = sortedWord;
                                if (!wordList.ContainsKey(temp)) //if no key
                                {
                                    wordList.Add(temp, new List<string>());
                                    wordList[temp].Add(readWord);
                                }
                                else
                                {
                                    wordList[temp].Add(readWord);
                                }
                            }
                            else
                            {
                                wordList[temp].Add(readWord); // dunno why this made it work
                            }
                        }
                    }
                }
               
                return wordList;
            }
            else
            {
                return Dict;
            }

        }
        public List<string> SearchRepository(string input) //good place for this? have to add it to interface so I can access through controller
        {
            var resultList = new List<string>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = new SqlCommand("SELECT * FROM Words WHERE Word LIKE '%' + @word+ '%'", connection);
                cmd.Parameters.Add(new SqlParameter("@word", SqlDbType.NVarChar));
                cmd.Parameters[0].Value = input;
                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var readWord = dr.GetString(1);
                        resultList.Add(readWord);
                    }
                }

            }
            return resultList;
        }
    }
}

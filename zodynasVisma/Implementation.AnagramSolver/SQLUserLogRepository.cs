using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AnagramSolver.Contracts;
using AnagramSolver.Models;

namespace AnagramSolver.BusinessLogic
{
    public class SQLUserLogRepository : IUserLogRepository
    {
        //public UserLogModel userLogModel;
        public void StoreUserInfo(UserLogModel userLog)
        {
            using (var connection = new SqlConnection("Server=LT-LIT-SC-0116\\ANAGRAMSOLVER; Database=Dictionary; Integrated Security=true"))
            {
                var splitInput = userLog.SearchedWord.Split(" ");

                connection.Open();
                var cmd = new SqlCommand("INSERT INTO UserLog (User_Ip, User_Searched_Word, Search_Time) VALUES (@userIP, @searchedWord, @searchTime)", connection);

                cmd.Parameters.Add(new SqlParameter("@userIP", SqlDbType.NVarChar));
                cmd.Parameters.Add(new SqlParameter("@searchedWord", SqlDbType.NVarChar));
                cmd.Parameters.Add(new SqlParameter("@searchTime", SqlDbType.Int));
                cmd.Parameters[0].Value = userLog.IPAdress; 
                cmd.Parameters[2].Value = userLog.SearchTime;
                foreach(string word in splitInput)
                {
                    cmd.Parameters[1].Value = word;
                    cmd.ExecuteNonQuery();
                }
                
            }
        }

        public UserLogModel GetUserLog(string userIP)
        {
            var userLog = new UserLogModel();
            using (var connection = new SqlConnection("Server=LT-LIT-SC-0116\\ANAGRAMSOLVER; Database=Dictionary; Integrated Security=true"))
            {
                connection.Open();
                var cmd = new SqlCommand("SELECT UserLog.User_IP, UserLog.Search_Time, UserLog.User_Searched_Word, Words.WordFROM ((UserLogINNER JOIN CachedWords ON UserLog.User_Searched_Word = CachedWords.Searched_Word)INNER JOIN Words ON CachedWords.Anagram_Word_ID = Words.ID) WHERE UserLog.User_IP =@userIP", connection);
                cmd.Parameters.Add(new SqlParameter("@userIP", SqlDbType.NVarChar));
                var dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        userLog.IPAdress = dr.GetString(0);
                    }
                }
            }
            return new UserLogModel();
        }
    }
}

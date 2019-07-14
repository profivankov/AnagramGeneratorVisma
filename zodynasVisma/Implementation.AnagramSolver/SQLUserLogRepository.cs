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
                cmd.Parameters.Add(new SqlParameter("@searchTime", SqlDbType.DateTime));
                cmd.Parameters[0].Value = userLog.IPAdress;
                cmd.Parameters[2].Value = userLog.SearchTime;
                foreach (string word in splitInput)
                {
                    cmd.Parameters[1].Value = word;
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public List<UserLogModel> GetUserLog(string userIP)
        {
            // var userLog = new UserLogModel();
            var userLogList = new List<UserLogModel>();
            using (var connection = new SqlConnection("Server=LT-LIT-SC-0116\\ANAGRAMSOLVER; Database=Dictionary; Integrated Security=true"))
            {
                connection.Open();
                var cmd = new SqlCommand("SELECT UserLog.User_IP, UserLog.Search_Time, UserLog.User_Searched_Word, Words.Word FROM ((UserLog INNER JOIN CachedWords ON UserLog.User_Searched_Word = CachedWords.Searched_Word) INNER JOIN Words ON CachedWords.Anagram_Word_ID = Words.ID) WHERE UserLog.User_IP =@userIP ORDER BY Search_Time", connection);
                cmd.Parameters.Add(new SqlParameter("@userIP", SqlDbType.NVarChar));
                cmd.Parameters[0].Value = userIP;
                var dr = cmd.ExecuteReader();
                var temp = "";

                if (dr.HasRows)
                {
                    dr.Read();
                    bool running = true;
                    while (running)
                    {
                        var userLog = new UserLogModel { AnagramWord = new List<string>() };
                        userLog.IPAdress = dr.GetString(0);
                        userLog.SearchedWord = dr.GetString(2);
                        userLog.SearchTime = dr.GetDateTime(1);

                        var searchedWord = dr.GetString(2);
                        var searchDate = dr.GetDateTime(1);
                        while (searchedWord == userLog.SearchedWord && searchDate == userLog.SearchTime)
                        {
                            userLog.AnagramWord.Add(dr.GetString(3));
                            if (dr.Read() == false)
                            {
                                running = false;
                                break;
                            }
                            searchedWord = dr.GetString(2);
                            searchDate = dr.GetDateTime(1);
                        }
                        userLogList.Add(userLog);
                    }
                }
            }
            return userLogList;
        }
    }
}

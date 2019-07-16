using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.Contracts;
using AnagramSolver.EF.DatabaseFirst.Models;
using AnagramSolver.Models;

namespace AnagramSolver.EF.DatabaseFirst.Repositories
{
    public class EFUserLogRepository : IUserLogRepository
    {
        private DictionaryContext _dbContext;
        public EFUserLogRepository()
        {
            _dbContext = new DictionaryContext();

        }
        public List<UserLogModel> GetUserLog(string userIP)
        {
            //var userLogList = new List<UserLogModel>();
            //var userLog = new UserLogModel { AnagramWord = new List<string>() };
            //var temp = userLog;

            //var query = from userlog in db.UserLog
            //            join cache in db.CachedWords on userlog.UserSearchedWord equals cache.SearchedWord
            //            where userlog.UserIp == userIP
            //            orderby userlog.SearchTime
            //            select new UserLogModel { IPAdress = userlog.UserIp, SearchTime = userlog.SearchTime, SearchedWord = userlog.UserSearchedWord, AnagramWord = };

            var userLogList = _dbContext.UserLog.Select(
                x => new UserLogModel
                {
                    IPAdress = x.UserIp,
                    SearchTime = x.SearchTime,
                    SearchedWord = x.UserSearchedWord,
                    AnagramWord = _dbContext.CachedWords.Where(y => y.SearchedWord == x.UserSearchedWord).Select(y => y.AnagramWord.Word).ToList()
                }).ToList(); //where to enter string userIP?

            //foreach(var row in query)
            //{
            //    userLog = new UserLogModel { AnagramWord = new List<string>() };
            //    userLog.IPAdress = row.col1;
            //    userLog.SearchTime = row.col2;
            //    userLog.SearchedWord = row.col3;
            //    if (!userLogList.Any(b=> b.SearchTime == userLog.SearchTime && b.SearchedWord == userLog.SearchedWord)) // this took me so long to think of 
            //    {
            //        foreach (var entry in query.Where(b => b.col2 == userLog.SearchTime && b.col3 == userLog.SearchedWord))
            //        {
            //            userLog.AnagramWord.Add(entry.col4);
            //        }
            //    }
            //    else
            //    {
            //        continue;
            //    }
            //    userLogList.Add(userLog);
            //}

            //foreach (var row in query)
            //{


            //    if (temp.SearchedWord != userLog.SearchedWord)
            //    {
            //        temp = userLog;
            //        userLogList.Add(userLog);

            //    }
            //    else
            //    {
            //        userLog.IPAdress = row.col1;
            //        userLog.SearchTime = row.col2;
            //        userLog.SearchedWord = row.col3;
            //        userLog.AnagramWord.Clear();

            //        var searchedWord = userLog.SearchedWord;
            //        var searchDate = userLog.SearchTime;

            //        foreach (var entry in query.Where(b => b.col2 == searchDate && b.col3 == searchedWord)) //  && !userLog.AnagramWord.Contains(b.col4))
            //        {
            //            userLog.AnagramWord.Add(entry.col4);
            //        }
            //    }

            //    //
            //}

            return userLogList;
        }

        public void StoreUserInfo(UserLogModel receivedUserLog)
        {
            var splitInput = receivedUserLog.SearchedWord.Split(" ");
            var userLog = new UserLog();

            foreach (string word in splitInput)
            {
                userLog = new UserLog();
                userLog.UserIp = receivedUserLog.IPAdress;
                userLog.SearchTime = receivedUserLog.SearchTime;
                userLog.UserSearchedWord = word;
                db.UserLog.Add(userLog);
                db.SaveChanges();
            }
        }
    }
}

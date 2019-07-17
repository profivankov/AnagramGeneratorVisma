using System;
using System.Collections.Generic;
using System.Linq;
using AnagramSolver.Contracts;
using AnagramSolver.EF.CodeFirst;
using AnagramSolver.EF.CodeFirst.Entities;
using AnagramSolver.Models;

namespace AnagramSolver.EF.DatabaseFirst.Repositories
{
    public class EFCFUserLogRepository : IUserLogRepository
    {
        private DictionaryContext _dbContext;
        public EFCFUserLogRepository(DictionaryContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<UserLogModelCF> GetUserLog(string userIP)
        {

            var userLogList = _dbContext.UserLog.Select(
                x => new UserLogModelCF
                {
                    IPAdress = x.UserIP,
                    SearchTime = x.SearchTime,
                    SearchedWordID = x.SearchedWordID,
                    AnagramWord = _dbContext.CachedWords.Where(y => y.SearchedWordID == x.SearchedWordID).Select(y => y.Words.Word).ToList()
                }).ToList(); //where to enter string userIP?


            return userLogList;
        }

        public void StoreUserInfo(string userIP, string input)
        {

            var userLog = new UserLog();
            userLog.UserIP = userIP;
            userLog.SearchTime = DateTime.Now;
            userLog.SearchedWordID = _dbContext.SearchedWords.Where(x => x.SearchedWord == input).Select(x => x.SearchedWordId).FirstOrDefault(); ;
            _dbContext.UserLog.Add(userLog);
            //_dbContext.SaveChanges();

        }

        public void StoreUserInfo(UserLogModel userLog)
        {
            throw new NotImplementedException();
        }

        List<UserLogModel> IUserLogRepository.GetUserLog(string userIP)
        {
            throw new NotImplementedException();
        }
    }
}

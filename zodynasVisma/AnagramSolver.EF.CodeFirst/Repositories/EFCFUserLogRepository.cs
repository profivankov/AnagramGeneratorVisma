﻿using System;
using System.Collections.Generic;
using System.Linq;
using AnagramSolver.Contracts;
using AnagramSolver.EF.CodeFirst;
using AnagramSolver.EF.CodeFirst.Entities;
using AnagramSolver.Models;

namespace AnagramSolver.EF.CodeFirst.Repositories
{
    public class EFCFUserLogRepository : IUserLogRepository
    {
        private DictionaryContext _dbContext;
        public EFCFUserLogRepository(DictionaryContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<UserLogModel> GetUserLog(string userIP)
        {
            var userLogList = _dbContext.UserLog.Where(x => x.UserIP == userIP).Select(
                x => new UserLogModel
                {
                    IPAdress = x.UserIP,
                    SearchTime = x.SearchTime,
                    SearchedWord = _dbContext.SearchedWords.Where( y => y.SearchedWordId == x.SearchedWordID ).Select(y => y.SearchedWord).First(),
                    AnagramWord = _dbContext.CachedWords.Where(y => y.SearchedWordID == x.SearchedWordID).Select(y => y.Words.Word).ToList()
                }).ToList(); 
            return userLogList;
        }

        public void StoreUserInfo(string userIP, string input)
        {
            var userLog = new UserLog();
            userLog.UserIP = userIP;
            userLog.SearchTime = DateTime.Now;
            userLog.SearchedWordID = _dbContext.SearchedWords.Where(x => x.SearchedWord == input).Select(x => x.SearchedWordId).FirstOrDefault(); ;
            _dbContext.UserLog.Add(userLog);
            _dbContext.SaveChanges();
        }


    }
}

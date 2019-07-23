using AnagramSolver.EF.CodeFirst.Contracts;
using AnagramSolver.EF.CodeFirst.Entities;
using AnagramSolver.Models;
using System;
using System.Collections.Generic;

namespace AnagramSolver.WebApp.Services
{
    public class UserLogService : IUserLogService
    {
        private IUserLogRepository _userLogRepository;
        private ICacheRepository _cacheRepository;

        public UserLogService(IUserLogRepository userLogRepository, ICacheRepository cacheRepository)
        {
            _userLogRepository = userLogRepository;
            _cacheRepository = cacheRepository;
        }
        public void AddToUserLog(string userIpAddress, string input)
        {
            foreach (string word in input.Split(" "))  //add to userLog
            {
                StoreUserInfo(userIpAddress, word);
            }
        }

        public void StoreUserInfo(string userIP, string input)
        {
            var userLog = new UserLog();
            userLog.UserIP = userIP;
            userLog.SearchTime = DateTime.Now;
            userLog.SearchedWordID = _cacheRepository.GetSearchedWordID(input);
            _userLogRepository.AddUserLog(userLog);
        }

        public List<UserLogModel> GetUserLog(string userIP)
        {

            return _userLogRepository.GetUserLog(userIP);
        }

    }
}

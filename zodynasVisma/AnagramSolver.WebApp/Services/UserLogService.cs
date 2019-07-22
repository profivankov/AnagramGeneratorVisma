using AnagramSolver.Contracts;
using AnagramSolver.EF.CodeFirst.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramSolver.WebApp.Services
{
    public class UserLogService : IUserLogService
    {
        private IUserLogRepository _userLogRepository;

        public UserLogService(IUserLogRepository userLogRepository)
        {
            _userLogRepository = userLogRepository;
        }
        public void AddToUserLog(string userIpAddress, string input)
        {
            foreach (string word in input.Split(" "))  //add to userLog
            {
                _userLogRepository.StoreUserInfo(userIpAddress, word);
            }
        }
    }
}

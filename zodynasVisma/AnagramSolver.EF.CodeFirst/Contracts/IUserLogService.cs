using AnagramSolver.Models;
using System.Collections.Generic;

namespace AnagramSolver.EF.CodeFirst.Contracts
{
    public interface IUserLogService
    {
        void AddToUserLog(string userIpAddress, string input);
        void StoreUserInfo(string userIP, string input);
        List<UserLogModel> GetUserLog(string userIP);
    }
}

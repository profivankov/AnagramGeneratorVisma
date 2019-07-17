using AnagramSolver.Models;
using System.Collections.Generic;

namespace AnagramSolver.Contracts
{
    public interface IUserLogRepository
    {
        void StoreUserInfo(string userIP, string input);
        List<UserLogModel> GetUserLog(string userIP);
    }
}

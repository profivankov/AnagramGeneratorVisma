using AnagramSolver.Models;
using System.Collections.Generic;

namespace AnagramSolver.Contracts
{
    public interface IUserLogRepository
    {
        void StoreUserInfo(UserLogModel userLog);
        List<UserLogModel> GetUserLog(string userIP);
    }
}

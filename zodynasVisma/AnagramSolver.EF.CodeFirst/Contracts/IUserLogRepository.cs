using AnagramSolver.EF.CodeFirst.Entities;
using AnagramSolver.Models;
using System.Collections.Generic;

namespace AnagramSolver.EF.CodeFirst.Contracts
{
    public interface IUserLogRepository
    {
        List<UserLogModel> GetUserLog(string userIP);

        void AddUserLog(UserLog userLog);
    }
}

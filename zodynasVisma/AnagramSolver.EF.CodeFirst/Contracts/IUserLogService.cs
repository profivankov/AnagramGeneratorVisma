using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.EF.CodeFirst.Contracts
{
    public interface IUserLogService
    {
        void AddToUserLog(string userIpAddress, string input);
    }
}

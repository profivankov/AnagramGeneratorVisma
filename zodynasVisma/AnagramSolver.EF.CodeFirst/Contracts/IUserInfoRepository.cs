using AnagramSolver.EF.CodeFirst.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.EF.CodeFirst.Contracts
{
    public interface IUserInfoRepository
    {
        string CheckForUserIP(string userIP);
        void AddUserInfo(UserInfo userInfo);
        void UpdateUserInfo(UserInfo userInfo);
        int GetSearchesByIP(string userIP);
        int GetTotalSearchesByIP(string userIP);
    }
}

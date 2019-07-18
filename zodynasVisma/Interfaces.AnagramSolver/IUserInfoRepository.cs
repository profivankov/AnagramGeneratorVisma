using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Contracts
{
    public interface IUserInfoRepository
    {
        int GetUserInfo();
        void NewUser();
        int AllowedSearches();
        void UpdateUserInfo();
    }
}

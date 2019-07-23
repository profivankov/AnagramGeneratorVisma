using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.EF.CodeFirst.Contracts
{
    public interface IUserInfoService
    {
        int GetUserInfo();
        void NewUser();
        void UpdateSearchAmount();
        void AddRemoveSearches(bool AddRemove);
    }
}

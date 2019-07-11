using AnagramSolver.Models;

namespace AnagramSolver.Contracts
{
    public interface IUserLogRepository
    {
        void StoreUserInfo(UserLogModel userLog);
        UserLogModel GetUserLog(string userIP);
    }
}

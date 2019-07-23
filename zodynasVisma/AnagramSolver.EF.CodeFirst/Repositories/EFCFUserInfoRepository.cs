using AnagramSolver.EF.CodeFirst.Contracts;
using AnagramSolver.EF.CodeFirst.Entities;
using System.Linq;

namespace AnagramSolver.EF.CodeFirst.Repositories
{
    public class EFCFUserInfoRepository : IUserInfoRepository
    {
        private DictionaryContext _dbContext;

        public EFCFUserInfoRepository(DictionaryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string CheckForUserIP(string userIP)
        {
            return _dbContext.UserInfo.Where(x => x.UserIP == userIP).Select(x => x.UserIP).FirstOrDefault();
        }

        public void AddUserInfo(UserInfo userInfo)
        {
            _dbContext.UserInfo.Add(userInfo);
            _dbContext.SaveChanges();
        }

        public void UpdateUserInfo(UserInfo userInfo)
        {
            _dbContext.UserInfo.Update(userInfo);
            _dbContext.SaveChanges();
        }

        public int GetSearchesByIP(string userIP)
        {
            return _dbContext.UserInfo.Where(x => x.UserIP == userIP).Select(x => x.SearchesLeft).First();
        }

        public int GetTotalSearchesByIP(string userIP)
        {
            return _dbContext.UserInfo.Where(x => x.UserIP == userIP).Select(x => x.TotalSearches).First();
        }
    }
}

using AnagramSolver.Contracts;
using AnagramSolver.EF.CodeFirst.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramSolver.EF.CodeFirst.Repositories
{
    public class EFCFUserInfoRepository : IUserInfoRepository
    {
        private DictionaryContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string _userIP;
        public IConfiguration Configuration { get; }

        public EFCFUserInfoRepository(DictionaryContext dbContext, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _userIP = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            Configuration = configuration;
        }

        public int GetUserInfo()
        {
            var checkIP = _dbContext.UserInfo.Where(x => x.UserIP == _userIP).Select(x => x.UserIP).FirstOrDefault();
            //checkIP == _userIP ? NewUser() : AllowedSearches(); 
            if (checkIP == null)
            {
                NewUser();
            }

            return AllowedSearches();
        }

        public void NewUser()
        {
            var limitations = Configuration.GetSection("Limitations");
            var maxSearches = Int32.Parse(limitations["MaximumSearches"]);

            var userInfo = new UserInfo()
            {
                UserIP = _userIP,
                SearchesLeft = maxSearches,
                TotalSearches = 0

            };
            _dbContext.UserInfo.Add(userInfo);
            _dbContext.SaveChanges();
        }

        public int AllowedSearches()
        {
            var userSearches = _dbContext.UserInfo.Where(x => x.UserIP == _userIP).Select(x => x.SearchesLeft).First();
            return userSearches;
        }

        public void UpdateSearchAmount()
        {
            var userInfo = new UserInfo()
            {
                UserIP = _userIP,
                SearchesLeft = _dbContext.UserInfo.Where(x => x.UserIP == _userIP).Select(x => x.SearchesLeft).First() - 1,
                TotalSearches = _dbContext.UserInfo.Where(x => x.UserIP == _userIP).Select(x => x.TotalSearches).First() + 1

            };
            _dbContext.UserInfo.Update(userInfo);
            _dbContext.SaveChanges();
        }

        public void AddRemoveSearches(bool AddRemove)
        {
            var userInfo = new UserInfo();
            if (AddRemove) // Add or Edit
            {
                userInfo = new UserInfo()
                {
                    UserIP = _userIP,
                    SearchesLeft = _dbContext.UserInfo.Where(x => x.UserIP == _userIP).Select(x => x.SearchesLeft).First() + 1,
                    TotalSearches = _dbContext.UserInfo.Where(x => x.UserIP == _userIP).Select(x => x.TotalSearches).First()

                };
            }
            else //Remove
            {
                userInfo = new UserInfo()
                {
                    UserIP = _userIP,
                    SearchesLeft = _dbContext.UserInfo.Where(x => x.UserIP == _userIP).Select(x => x.SearchesLeft).First() - 1,
                    TotalSearches = _dbContext.UserInfo.Where(x => x.UserIP == _userIP).Select(x => x.TotalSearches).First()

                };
            }
            _dbContext.UserInfo.Update(userInfo);
            _dbContext.SaveChanges();
        }
    }
}

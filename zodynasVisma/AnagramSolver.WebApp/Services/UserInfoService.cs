using AnagramSolver.EF.CodeFirst.Contracts;
using AnagramSolver.EF.CodeFirst.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramSolver.WebApp.Services
{
    public class UserInfoService : IUserInfoService
    {
        private IUserInfoRepository _userInfoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IConfiguration Configuration { get; }
        private string _userIP;
        

        public UserInfoService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IUserInfoRepository userInfoRepository)
        {
            _userInfoRepository = userInfoRepository;
            _httpContextAccessor = httpContextAccessor;
            _userIP = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            Configuration = configuration;
        }

        public int GetUserInfo()
        {
            var checkIP = _userInfoRepository.CheckForUserIP(_userIP);
            if (checkIP == null)
            {
                NewUser();
            }
            return _userInfoRepository.GetSearchesByIP(_userIP);
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
            _userInfoRepository.AddUserInfo(userInfo);
        }

        public void UpdateSearchAmount()
        {
            var userInfo = new UserInfo()
            {
                UserIP = _userIP,
                SearchesLeft = _userInfoRepository.GetSearchesByIP(_userIP) - 1,
                TotalSearches = _userInfoRepository.GetTotalSearchesByIP(_userIP) + 1
            };
            _userInfoRepository.UpdateUserInfo(userInfo);
        }

        public void AddRemoveSearches(bool AddRemove)
        {
            var userInfo = new UserInfo();
            if (AddRemove) // Add or Edit
            {
                userInfo = new UserInfo()
                {
                    UserIP = _userIP,
                    SearchesLeft = _userInfoRepository.GetSearchesByIP(_userIP) + 1,
                    TotalSearches = _userInfoRepository.GetTotalSearchesByIP(_userIP)
                };
            }
            else //Remove
            {
                userInfo = new UserInfo()
                {
                    UserIP = _userIP,
                    SearchesLeft = _userInfoRepository.GetSearchesByIP(_userIP) - 1,
                    TotalSearches = _userInfoRepository.GetTotalSearchesByIP(_userIP)
                };
            }
            _userInfoRepository.UpdateUserInfo(userInfo);
        }
    }
}

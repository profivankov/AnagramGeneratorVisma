using System;
using System.Collections.Generic;
using System.Text;
using AnagramSolver.EF.CodeFirst.Contracts;
using AnagramSolver.EF.CodeFirst.Entities;
using AnagramSolver.Models;
using AnagramSolver.WebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;

namespace AnagramSolver.Tests.Services
{
    [TestFixture]
    public class UserInfoServiceTests
    {
        private IUserInfoRepository _userInfoRepository;
        private IHttpManagerService _httpManagerService;
        public IConfiguration configuration;
        private UserInfoService _userInfoService;
        private string _userIP;

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }

        [SetUp]
        public void Setup()
        {
            configuration = InitConfiguration();
            _httpManagerService = Substitute.For<IHttpManagerService>();
            _userInfoRepository = Substitute.For<IUserInfoRepository>();
            _userInfoService = new UserInfoService(_httpManagerService, configuration, _userInfoRepository);
            _userIP = "::1";
        }

        [Test]
        public void Should_ReturnSearchAmount_When_IPNotNull()
        {
            //mock
            _httpManagerService.GetIP().Returns(_userIP);
            _userInfoRepository.CheckForUserIP(Arg.Any<string>()).Returns(_userIP);
            _userInfoRepository.GetSearchesByIP(Arg.Any<string>()).Returns(50);
            _userInfoService = new UserInfoService(_httpManagerService, configuration, _userInfoRepository);

            //call
            var result = _userInfoService.GetUserInfo();

            //compare
            var expected = 50;
            Assert.AreEqual(expected, result);

            //check if
            _userInfoRepository.Received().CheckForUserIP(_userIP);
            _userInfoRepository.Received().GetSearchesByIP(Arg.Any<string>());
        }

        [Test]
        public void Should_CreateNewUser_When_IPNull()
        {
            //mock
            _httpManagerService.GetIP().Returns(string.Empty);
            _userInfoRepository.CheckForUserIP(Arg.Any<string>()).Returns(string.Empty);
            _userInfoRepository.GetSearchesByIP(Arg.Any<string>()).Returns(50);
            _userInfoService = new UserInfoService(_httpManagerService, configuration, _userInfoRepository);

            //call
            var result = _userInfoService.GetUserInfo();

            //compare
            var expected = 50;
            Assert.AreEqual(expected, result);

            //check if
            //_userInfoService.Received().NewUser(); how to check if other method of service was called
            _userInfoRepository.Received().CheckForUserIP(string.Empty);
            _userInfoRepository.Received().GetSearchesByIP(Arg.Any<string>());
        }

        [Test]
        public void Should_AddCorrectUserInfo_When_Called() // 
        {
            //mock

            _httpManagerService.GetIP().Returns(_userIP); // bcz its called in constructor, need to inject again?
           _userInfoService = new UserInfoService(_httpManagerService, configuration, _userInfoRepository);
            var maxSearches = Int32.Parse(configuration.GetSection("Limitations")["MaximumSearches"]);

            //call
            _userInfoService.NewUser();

            //check if same values were added
            _userInfoRepository.Received().AddUserInfo(Arg.Is<UserInfo>(x => x.UserIP.Equals(_userIP)));
            _userInfoRepository.Received().AddUserInfo(Arg.Is<UserInfo>(x => x.SearchesLeft.Equals(maxSearches)));
            _userInfoRepository.Received().AddUserInfo(Arg.Is<UserInfo>(x => x.TotalSearches.Equals(0)));

        }

        [Test]
        public void Should_UdateSearchAmount_When_Called()
        {
            var searchesLeft = 10;
            var totalSearches = 0;
            _httpManagerService.GetIP().Returns(_userIP); 
            _userInfoService = new UserInfoService(_httpManagerService, configuration, _userInfoRepository);
            _userInfoRepository.GetSearchesByIP(_userIP).Returns(searchesLeft);
            _userInfoRepository.GetTotalSearchesByIP(_userIP).Returns(totalSearches);

            _userInfoService.UpdateSearchAmount();

            _userInfoRepository.Received().UpdateUserInfo(Arg.Is<UserInfo>(x => x.UserIP.Equals(_userIP)));
            _userInfoRepository.Received().UpdateUserInfo(Arg.Is<UserInfo>(x => x.SearchesLeft.Equals(searchesLeft - 1)));
            _userInfoRepository.Received().UpdateUserInfo(Arg.Is<UserInfo>(x => x.TotalSearches.Equals(totalSearches + 1)));
        }
    }
}

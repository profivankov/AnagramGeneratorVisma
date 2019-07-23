using System;
using System.Collections.Generic;
using System.Text;
using AnagramSolver.EF.CodeFirst.Contracts;
using AnagramSolver.EF.CodeFirst.Entities;
using AnagramSolver.Models;
using AnagramSolver.WebApp.Services;
using NSubstitute;
using NUnit.Framework;

namespace AnagramSolver.Tests.Services
{
    [TestFixture]
    public class UserLogServiceTests
    {
        private IUserLogRepository _userLogRepository;
        private ICacheRepository _cacheRepository;
        private UserLogService _userLogService;
        private string _userIP;

        [SetUp]
        public void Setup()
        {
            _userLogRepository = Substitute.For<IUserLogRepository>();
            _cacheRepository = Substitute.For<ICacheRepository>();
            _userLogService = new UserLogService(_userLogRepository, _cacheRepository);
            _userIP = "::1";
        }


       // [Test]
        //public void Should_ReturnCorrectUserLog_When_Called()
        //{
        //    //mock
        //    _userLogRepository.GetUserLog(_userIP).Returns(new List<UserLogModel>
        //    {
        //        new UserLogModel
        //        {
        //            IPAdress = _userIP, SearchTime = new DateTime(2012, 12, 21), SearchedWord = "alus", AnagramWord = new List<string> { "alus", "sula" }
        //        }
        //    });

        //    //call
        //    var result = _userLogService.GetUserLog(_userIP);

        //    var expected = new List<UserLogModel>
        //    {
        //        new UserLogModel
        //        {
        //            IPAdress = _userIP,
        //            SearchTime = new DateTime(2012, 12, 21),
        //            SearchedWord = "alus",
        //            AnagramWord = new List<string> { "alus", "sula" }
        //        }
        //    };

        //    Assert.AreEqual(expected, result);
        //}
    }
}

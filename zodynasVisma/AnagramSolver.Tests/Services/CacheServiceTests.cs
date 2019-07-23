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
    public class CacheServiceTests
    {
        private ICacheRepository _cacheRepository;
        private IAnagramSolverService _anagramSolverService;
        private CacheService cacheService;

        [SetUp]
        public void Setup()
        {
            _anagramSolverService = Substitute.For<IAnagramSolverService>();
            _cacheRepository = Substitute.For<ICacheRepository>();
            cacheService = new CacheService(_anagramSolverService, _cacheRepository);
        }

        [Test]
        public void Should_ReturnCorrectList_When_MultipleWords()
        {
            //idk
        }

        [Test]
        public void Should_ReturnCorrectListFromCache_When_OneWord()
        {
            //mock
            var request = "alus";
            _cacheRepository.SearchCacheForAnagrams(request).Returns(new List<string> { "alus", "sula" });

            //call
            var result = cacheService.GetAnagramsFromCache(request);

            //compare results
            var expected = new List<string> { "alus", "sula" };
            Assert.AreEqual(expected, result);

            //check if called
            _cacheRepository.Received().SearchCacheForAnagrams(request);
        }
    }
}

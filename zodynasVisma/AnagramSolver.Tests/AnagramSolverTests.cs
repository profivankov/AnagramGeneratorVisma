using NUnit.Framework;
using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;
using System.Collections.Generic;
using System.IO;

namespace AnagramSolver.Tests
{
    [TestFixture]
    class AnagramSolverTests
    {
        private List<string> testList;
        BusinessLogic.AnagramSolver testObject;
        StreamReader testFile;

        [SetUp]
        public void Setup()
        {
            // Dictionary<string, List<string>> wordList = _wordRepository.GetDictionary();
            testObject = new BusinessLogic.AnagramSolver(new TestWordRepository(), 5); // max results = 5

            testList = new List<string>() { "alus", "sula", "labas", "balas" };
            
        }
        [TearDown]

        [Test]
        public void Should_ReturnEmptyList_When_EmptyInput()
        {
            var result = testObject.GetAnagrams(new string[]{}, testFile);
            Assert.AreEqual(result, new List<string>());
        }
        [Test]
        public void Should_ReturnEmptyList_When_NoSuchWord()
        {
            var result = testObject.GetAnagrams(new string[] { "asfa235325sfa" }, testFile);
            Assert.AreEqual(result, new List<string>());
        }
        [Test]
        public void Should_ReturnSpecificString_When_MaxResults() // 
        {
            var result = testObject.GetAnagrams(new string[]{ "alus", "dievas" }, testFile);
            Assert.AreEqual(result.Contains("MAXWORDSREACHED"),true);
        }
        [Test]
        public void Should_NotReturnSpecificString_When_NotMaxResults() // 
        {
            var result = testObject.GetAnagrams(new string[] { "alus" }, testFile);
            Assert.AreEqual(result.Contains("MAXWORDSREACHED"), false);
        }
        [Test]
        public void Should_ReturnAnagrams_When_Called()
        {
            var result = testObject.GetAnagrams(new string[] { "alus", "labas" }, testFile);
            CollectionAssert.AreEquivalent(testList, result);
        }

    }
}

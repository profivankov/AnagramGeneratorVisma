using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace AnagramSolver.Tests
{
    [TestFixture]
    class AnagramSolverTests
    {
        private List<string> testList;
        BusinessLogic.AnagramSolver testObject;


        [SetUp]
        public void Setup()
        {
            testObject = new BusinessLogic.AnagramSolver(new TestWordRepository()); // max results = 5
            testList = new List<string>() { "alus", "sula", "labas", "balas" };

        }
        //[TearDown]

        [Test]
        public void Should_ReturnEmptyList_When_EmptyInput()
        {
            var result = testObject.GetAnagrams(new string[]{});
            Assert.AreEqual(new List<string>(), result);
        }
        [Test]
        public void Should_ReturnEmptyList_When_NoSuchWord()
        {
            var result = testObject.GetAnagrams(new string[] { "asfa235325sfa" });
            Assert.AreEqual(new List<string>(), result);
        }
        [Test]
        public void Should_ReturnSpecificString_When_MaxResults() // 
        {
            var result = testObject.GetAnagrams(new string[]{ "alus", "dievas" });
            Assert.AreEqual(true, result.Contains("MAXWORDSREACHED"));
        }
        [Test]
        public void Should_NotReturnSpecificString_When_NotMaxResults() // 
        {
            var result = testObject.GetAnagrams(new string[] { "alus" });
            Assert.AreEqual(false, result.Contains("MAXWORDSREACHED"));
        }
        [Test]
        public void Should_ReturnAnagrams_When_Called()
        {
            var result = testObject.GetAnagrams(new string[] { "alus", "labas" });
            CollectionAssert.AreEquivalent(testList, result);
        }

    }
}

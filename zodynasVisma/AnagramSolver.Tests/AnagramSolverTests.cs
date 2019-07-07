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
        StreamReader testFile;


        [SetUp]
        public void Setup()
        {
            testObject = new BusinessLogic.AnagramSolver(new TestWordRepository(), 5); // max results = 5
            testList = new List<string>() { "alus", "sula", "labas", "balas" };

        }
        //[TearDown]

        [Test]
        public void Should_ReturnEmptyList_When_EmptyInput()
        {
            var result = testObject.GetAnagrams(new string[]{}, testFile);
            Assert.AreEqual(new List<string>(), result);
        }
        [Test]
        public void Should_ReturnEmptyList_When_NoSuchWord()
        {
            var result = testObject.GetAnagrams(new string[] { "asfa235325sfa" }, testFile);
            Assert.AreEqual(new List<string>(), result);
        }
        [Test]
        public void Should_ReturnSpecificString_When_MaxResults() // 
        {
            var result = testObject.GetAnagrams(new string[]{ "alus", "dievas" }, testFile);
            Assert.AreEqual(true, result.Contains("MAXWORDSREACHED"));
        }
        [Test]
        public void Should_NotReturnSpecificString_When_NotMaxResults() // 
        {
            var result = testObject.GetAnagrams(new string[] { "alus" }, testFile);
            Assert.AreEqual(false, result.Contains("MAXWORDSREACHED"));
        }
        [Test]
        public void Should_ReturnAnagrams_When_Called()
        {
            var result = testObject.GetAnagrams(new string[] { "alus", "labas" }, testFile);
            CollectionAssert.AreEquivalent(testList, result);
        }

    }
}

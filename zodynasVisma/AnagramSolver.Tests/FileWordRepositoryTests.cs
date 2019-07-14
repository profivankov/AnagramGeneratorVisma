using NUnit.Framework;
using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;
using System.Collections.Generic;
using System.IO;

namespace AnagramSolver.Tests
{
    [TestFixture]
    class FileWordRepositoryTests
    {
        private List<string> testList;
        BusinessLogic.AnagramSolver testObject;
        StreamReader testFile;

        [SetUp]
        public void Setup()
        {
            // Dictionary<string, List<string>> wordList = _wordRepository.GetDictionary();
            testObject = new BusinessLogic.AnagramSolver(new TestWordRepository()); // max results = 5
            //file = new StreamReader(@"C:\Users\mantrimas\source\repos\zodynasVisma\zodynasVisma\zodynas.txt");
            testFile = new StreamReader(@"C:\Users\mantrimas\source\repos\zodynasVisma\zodynasVisma\zodynasfortesting.txt"); // not used
            testList = new List<string>() { "alus", "sula", "labas", "balas" };
            //TEARDOWN for txt file

        }

        [Test]
        public void Should_ReturnEmptyList_When_EmptyInput()
        {
            var result = testObject.GetAnagrams(new string[] { });
            Assert.AreEqual(result, new List<string>());
        }
    }
}

using NUnit.Framework;
using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts;
using System.Collections.Generic;

namespace AnagramSolver.Tests
{
    [TestFixture]
    class AnagramSolverTests
    {
        private IWordRepository _wordRepository;
        AnagramGenerator testObject;
        public AnagramSolverTests()
        {
            _wordRepository = new FileWordRepository();
        }

        [SetUp]
        public void Setup()
        {
            // Dictionary<string, List<string>> wordList = _wordRepository.GetDictionary();
            testObject = new AnagramGenerator(new FileWordRepository());
        }

        [Test]
        public void Should_ReturnEmptyList_When_EmptyInput()
        {
            
            var result = testObject.GetAnagrams(new string[]{});
            Assert.AreEqual(result, new List<string>());
        }

    }
}

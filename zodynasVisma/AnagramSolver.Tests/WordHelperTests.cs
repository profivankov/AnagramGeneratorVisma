using NUnit.Framework;
using AnagramSolver.BusinessLogic;

namespace AnagramSolver.Tests
{
    [TestFixture]
    public class WordHelperTests
    {

        [Test]
        public void Should_ReturnFalse_When_WordLessThanMinLetters()
        {
            var result = WordHelper.CheckInput(new string[] { "a", "b" });
            Assert.AreEqual(result,false);
        }
        [Test]
        public void Should_ReturnTrue_When_WordHasMinLetters()
        {
            var result = WordHelper.CheckInput(new string[] { "as", "tu" });
            Assert.AreEqual(result, true);
        }



    }
}
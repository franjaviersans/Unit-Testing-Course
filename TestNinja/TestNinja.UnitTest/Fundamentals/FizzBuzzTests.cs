using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTest.Fundamentlas
{
    [TestFixture]
    class FizzBuzzTests
    {
        [Test]
        [TestCase(15)]
        [TestCase(30)]
        public void GetOutput_MultipleOf3And5_ReturnFizzBuzz(int number)
        {
            var result = FizzBuzz.GetOutput(number);

            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }

        [Test]
        [TestCase(3)]
        [TestCase(6)]
        public void GetOutput_MultipleOf3_ReturnFizz(int number)
        {
            var result = FizzBuzz.GetOutput(number);

            Assert.That(result, Is.EqualTo("Fizz"));
        }

        [Test]
        [TestCase(5)]
        [TestCase(10)]
        public void GetOutput_MultipleOf5_ReturnBuzz(int number)
        {
            var result = FizzBuzz.GetOutput(number);

            Assert.That(result, Is.EqualTo("Buzz"));
        }

        [Test]
        public void GetOutput_NotMultipleOf3Or5_ReturnStringNumber()
        {
            var result = FizzBuzz.GetOutput(73);

            Assert.That(result, Is.EqualTo("73"));
        }
    }
}

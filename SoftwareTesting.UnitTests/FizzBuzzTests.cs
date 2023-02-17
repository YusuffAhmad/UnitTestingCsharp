using SoftwareTesting.Fundamentals;

namespace SoftwareTesting.UnitTests
{
    [TestFixture]
    public class FizzBuzzTests
    {
        [Test]
        public void GetOutput_InputIsDivisibleBy3And5_RerturnFizzBuzz()
        {
            int input = 15;

            var result = FizzBuzz.GetOutput(input);

            Assert.That(result, Is.EqualTo("FizzBuzz").IgnoreCase);
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetOutput_InputIsDivisibleBy3_RerturnFizz()
        {
            int input = 3;

            var result = FizzBuzz.GetOutput(input);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo("Fizz").IgnoreCase);
        }

        [Test]
        public void GetOutput_InputIsDivisibleBy5_RerturnBuzz()
        {
            int input = 5;

            var result = FizzBuzz.GetOutput(input);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo("Buzz").IgnoreCase);
        }
        
        [Test]
        public void GetOutput_InputIsNotDivisibleBy3Or5_RerturnTheSameNumber()
        {
            int input = 7;

            var result = FizzBuzz.GetOutput(input);

            Assert.That(result, Is.EqualTo(input.ToString()));
        }
    }
}


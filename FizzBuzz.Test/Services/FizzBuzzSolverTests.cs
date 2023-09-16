using FizzBuzz.Api.Models;
using FizzBuzz.Api.Services;

namespace FizzBuzz.Test.Services
{
    [TestFixture]
    public class FizzBuzzSolverTests
    {
        [Test]
        public void SolveFizzBuzz_ReturnsCorrectResults()
        {
            // Arrange
            var fizzBuzzSolver = new FizzBuzzSolver();
            var fizzBuzzRequest = new FizzBuzzRequest
            {
                MaxNumber = 15,
                FizzBuzzValues = new Dictionary<int, string>
            {
                { 3, "Fizz" },
                { 5, "Buzz" }
            }
            };

            // Act
            var fizzBuzzResponse = fizzBuzzSolver.SolveFizzBuzz(fizzBuzzRequest);

            // Assert
            var expectedResults = new List<string>
        {
            "1",
            "2",
            "Fizz",
            "4",
            "Buzz",
            "Fizz",
            "7",
            "8",
            "Fizz",
            "Buzz",
            "11",
            "Fizz",
            "13",
            "14",
            "FizzBuzz"
        };

            CollectionAssert.AreEqual(expectedResults, fizzBuzzResponse.FizzBuzzResults);
        }

        [Test]
        public void SolveFizzBuzz_MaxNumberIsZero_ReturnsEmptyResults()
        {
            // Arrange
            var fizzBuzzSolver = new FizzBuzzSolver();
            var fizzBuzzRequest = new FizzBuzzRequest
            {
                MaxNumber = 0,
                FizzBuzzValues = new Dictionary<int, string>
            {
                { 3, "Fizz" },
                { 5, "Buzz" }
            }
            };

            // Act
            var fizzBuzzResponse = fizzBuzzSolver.SolveFizzBuzz(fizzBuzzRequest);

            // Assert
            Assert.IsEmpty(fizzBuzzResponse.FizzBuzzResults);
        }
    }
}
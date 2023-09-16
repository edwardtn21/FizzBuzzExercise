using FizzBuzz.Api.Models;

namespace FizzBuzz.Test.Models
{
    [TestFixture]
    public class FizzBuzzResponseTests
    {
        [Test]
        public void FizzBuzzResults_Property_SetGet()
        {
            // Arrange
            var fizzBuzzResponse = new FizzBuzzResponse();
            var expectedResults = new List<string> { "1", "2", "Fizz", "4", "Buzz" };

            // Act
            fizzBuzzResponse.FizzBuzzResults = expectedResults;

            // Assert
            CollectionAssert.AreEqual(expectedResults, fizzBuzzResponse.FizzBuzzResults);
        }
    }
}

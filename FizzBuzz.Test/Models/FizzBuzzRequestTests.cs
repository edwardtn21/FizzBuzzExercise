using FizzBuzz.Api.Models;


namespace FizzBuzz.Test.Models
{
    [TestFixture]
    public class FizzBuzzRequestTests
    {
        [Test]
        public void MaxNumber_Property_SetGet()
        {
            // Arrange
            var fizzBuzzRequest = new FizzBuzzRequest();
            var expectedMaxNumber = 100;

            // Act
            fizzBuzzRequest.MaxNumber = expectedMaxNumber;

            // Assert
            Assert.AreEqual(expectedMaxNumber, fizzBuzzRequest.MaxNumber);
        }

        [Test]
        public void FizzBuzzValues_Property_SetGet()
        {
            // Arrange
            var fizzBuzzRequest = new FizzBuzzRequest();
            var expectedFizzBuzzValues = new Dictionary<int, string>
        {
            { 3, "Fizz" },
            { 5, "Buzz" }
        };

            // Act
            fizzBuzzRequest.FizzBuzzValues = expectedFizzBuzzValues;

            // Assert
            CollectionAssert.AreEqual(expectedFizzBuzzValues, fizzBuzzRequest.FizzBuzzValues);
        }
    }
}

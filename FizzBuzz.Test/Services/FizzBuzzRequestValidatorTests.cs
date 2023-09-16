using FluentValidation.TestHelper;
using FizzBuzz.Api.Models;
using FizzBuzz.Api.Services;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FizzBuzz.Test.Services
{
    [TestFixture]
    public class FizzBuzzRequestValidatorTests
    {
        private FizzBuzzRequestValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new FizzBuzzRequestValidator();
        }

        [Test]
        public void MaxNumber_ValidRange_PassesValidation()
        {
            // Arrange
            var fizzBuzzRequest = new FizzBuzzRequest
            {
                MaxNumber = 100,
                FizzBuzzValues = new Dictionary<int, string>
            {
                { 3, "Fizz" },
                { 5, "Buzz" }
            }
            };

            // Act & Assert
            var result = _validator.TestValidate(fizzBuzzRequest);
            result.ShouldNotHaveValidationErrorFor(x => x.MaxNumber);            
        }

        [Test]
        public void MaxNumber_OutOfRange_FailsValidation()
        {
            // Arrange
            var fizzBuzzRequest = new FizzBuzzRequest
            {
                MaxNumber = 1000, // This is out of the valid range (0 to 500)
                FizzBuzzValues = new System.Collections.Generic.Dictionary<int, string>
            {
                { 3, "Fizz" },
                { 5, "Buzz" }
            }
            };

            // Act & Assert
            var result = _validator.TestValidate(fizzBuzzRequest);
            result.ShouldHaveValidationErrorFor(x => x.MaxNumber)
                .WithErrorMessage("'Max Number' must be between 0 and 500. You entered 1000.");
        }

        [Test]
        public void FizzBuzzValues_NotNull_PassesValidation()
        {
            // Arrange
            var fizzBuzzRequest = new FizzBuzzRequest
            {
                MaxNumber = 100,
                FizzBuzzValues = new System.Collections.Generic.Dictionary<int, string>
            {
                { 3, "Fizz" },
                { 5, "Buzz" }
            }
            };

            // Act & Assert
            var result = _validator.TestValidate(fizzBuzzRequest);
            result.ShouldNotHaveValidationErrorFor(x => x.FizzBuzzValues);
        }

        [Test]
        public void FizzBuzzValues_Null_FailsValidation()
        {
            // Arrange
            var fizzBuzzRequest = new FizzBuzzRequest
            {
                MaxNumber = 100,
                FizzBuzzValues = null
            };

            // Act & Assert
            var result = _validator.TestValidate(fizzBuzzRequest);
            result.ShouldHaveValidationErrorFor(x => x.FizzBuzzValues)
                .WithErrorMessage("'Fizz Buzz Values' must not be empty.");
        }
    }
}

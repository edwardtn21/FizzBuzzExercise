using NUnit.Framework;
using Moq;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using FizzBuzz.Api.Controllers;
using FizzBuzz.Api.Interfaces;
using FizzBuzz.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FizzBuzz.Test.Controllers
{
    [TestFixture]
    public class FizzBuzzControllerTests
    {
        private FizzBuzzController _controller;
        private Mock<IFizzBuzzLogger> _loggerMock;
        private Mock<IValidator<FizzBuzzRequest>> _validatorMock;
        private Mock<IFizzBuzzSolver> _solverMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<IFizzBuzzLogger>();
            _validatorMock = new Mock<IValidator<FizzBuzzRequest>>();
            _solverMock = new Mock<IFizzBuzzSolver>();
            _controller = new FizzBuzzController(_loggerMock.Object, _validatorMock.Object, _solverMock.Object);
        }

        [Test]
        public async Task SolveFizzBuzz_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var fizzBuzzRequest = new FizzBuzzRequest
            {
                MaxNumber = 15,
                FizzBuzzValues = new Dictionary<int, string>
        {
            { 3, "Fizz" },
            { 5, "Buzz" }
        }
            };

            _validatorMock.Setup(v => v.ValidateAsync(fizzBuzzRequest, new CancellationToken())).ReturnsAsync(new ValidationResult());

            var expectedResponse = new FizzBuzzResponse
            {
                FizzBuzzResults = new List<string> { "1", "2", "Fizz", "4", "Buzz" }
            };

            _solverMock.Setup(s => s.SolveFizzBuzz(fizzBuzzRequest)).Returns(expectedResponse);

            // Act
            var result = await _controller.SolveFizzBuzz(fizzBuzzRequest);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(expectedResponse, okResult.Value);
            _loggerMock.Verify(logger => logger.LogInformation(It.IsAny<string>()), Times.AtLeastOnce);
            _loggerMock.Verify(logger => logger.LogError(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task SolveFizzBuzz_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var fizzBuzzRequest = new FizzBuzzRequest(); // An invalid request without required properties set

            var validationFailures = new List<ValidationFailure>
    {
        new ValidationFailure("MaxNumber", "The MaxNumber field is required.")
    };

            _validatorMock.Setup(v => v.ValidateAsync(fizzBuzzRequest, new CancellationToken()))
                .ReturnsAsync(new ValidationResult(validationFailures));

            // Act
            var result = await _controller.SolveFizzBuzz(fizzBuzzRequest);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
            _loggerMock.Verify(logger => logger.LogError(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task SolveFizzBuzz_SolverThrowsException_ReturnsInternalServerError()
        {
            // Arrange
            var fizzBuzzRequest = new FizzBuzzRequest
            {
                MaxNumber = 15,
                FizzBuzzValues = new Dictionary<int, string>
        {
            { 3, "Fizz" },
            { 5, "Buzz" }
        }
            };

            _validatorMock.Setup(v => v.ValidateAsync(fizzBuzzRequest, new CancellationToken())).ReturnsAsync(new ValidationResult());

            _solverMock.Setup(s => s.SolveFizzBuzz(fizzBuzzRequest)).Throws(new Exception("Simulated error"));

            // Act
            var result = await _controller.SolveFizzBuzz(fizzBuzzRequest);

            // Assert
            Assert.IsInstanceOf<StatusCodeResult>(result);
            var statusCodeResult = (StatusCodeResult)result;
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            _loggerMock.Verify(logger => logger.LogError(It.IsAny<string>()), Times.Once);
        }
    }
}
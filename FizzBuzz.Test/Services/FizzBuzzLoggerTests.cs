using FizzBuzz.Api.Controllers;
using FizzBuzz.Api.Database;
using FizzBuzz.Api.Database.Models;
using FizzBuzz.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzz.Test.Services
{
    [TestFixture]
    public class FizzBuzzLoggerIntegrationTests
    {
        private FizzBuzzLogger _loggerService;
        private DbContextOptions<LoggerDbContext> _dbOptions;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // Set up the test database context
            _dbOptions = new DbContextOptionsBuilder<LoggerDbContext>()
                .UseInMemoryDatabase(databaseName: "FizzBuzzLoggerTest")
                .Options;

            using var context = new LoggerDbContext(_dbOptions);
            // Seed the in-memory database with initial data if needed
            context.LogEntries.Add(new LogEntry { DateTime = System.DateTimeOffset.Now, Type = "Information", Message = "Test message" });
            context.SaveChanges();
        }

        [SetUp]
        public void Setup()
        {
            // Create a new instance of the logger service with the test database context
            var context = new LoggerDbContext(_dbOptions);                        
            LoggerFactory loggerFactory = new();
            ILogger<FizzBuzzController> logger = loggerFactory.CreateLogger<FizzBuzzController>();
            _loggerService = new FizzBuzzLogger(logger, context);
        }

        [Test]
        public void Log_Information_LogsToDatabaseAndLogger()
        {
            // Arrange
            string message = "This is an information message";

            // Act
            _loggerService.Log(message);

            // Assert
            using var context = new LoggerDbContext(_dbOptions);
            var logEntry = context.LogEntries.Last();
            Assert.That(logEntry, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(logEntry.Message, Is.EqualTo(message));
                Assert.That(logEntry.Type, Is.EqualTo("Information"));
            });
        }

        [Test]
        public void Log_Error_LogsToDatabaseAndLogger()
        {
            // Arrange
            string errorMessage = "This is an error message";

            // Act
            _loggerService.LogError(errorMessage);

            // Assert
            using var context = new LoggerDbContext(_dbOptions);
            var logEntry = context.LogEntries.Last();
            Assert.That(logEntry, Is.Not.Null);
            Assert.That(logEntry.Message, Is.EqualTo(errorMessage));
            Assert.That(logEntry.Type, Is.EqualTo("Error"));
            
        }

        [Test]
        public void Log_WithCustomType_LogsToDatabaseAndLogger()
        {
            // Arrange
            string message = "This is a custom type message";
            string customType = "CustomType";

            // Act
            _loggerService.Log(message, customType);

            // Assert
            using var context = new LoggerDbContext(_dbOptions);
            var logEntry = context.LogEntries.Last();
            Assert.That(logEntry, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(logEntry.Message, Is.EqualTo(message));
                Assert.That(logEntry.Type, Is.EqualTo(customType));
            });
        }
    }
}

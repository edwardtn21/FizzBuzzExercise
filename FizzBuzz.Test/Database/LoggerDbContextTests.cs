using Microsoft.EntityFrameworkCore;
using FizzBuzz.Api.Database;
using FizzBuzz.Api.Database.Models;

namespace FizzBuzz.Test.Database
{
    [TestFixture]
    public class LoggerDbContextIntegrationTests
    {
        private DbContextOptions<LoggerDbContext> _dbOptions;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // Set up the test database context
            _dbOptions = new DbContextOptionsBuilder<LoggerDbContext>()
                .UseInMemoryDatabase(databaseName: "LoggerDbContextTest")
                .Options;

            using var context = new LoggerDbContext(_dbOptions);
            // Seed the in-memory database with initial data if needed
            context.LogEntries.Add(new LogEntry { DateTime = System.DateTimeOffset.Now, Type = "Information", Message = "Test message" });
            context.SaveChanges();
        }

        [Test]
        public void LogEntries_CanBeRetrievedFromDatabase()
        {
            // Arrange
            using var context = new LoggerDbContext(_dbOptions);
            // Act
            var logEntries = context.LogEntries.ToList();

            // Assert
            Assert.That(logEntries, Is.Not.Null);
            Assert.That(logEntries, Has.Count.EqualTo(1));
            // Additional assertions on log entries as needed
        }

        [Test]
        public void LogEntry_CanBeAddedToDatabase()
        {
            // Arrange
            using var context = new LoggerDbContext(_dbOptions);
            var newLogEntry = new LogEntry
            {
                DateTime = System.DateTimeOffset.Now,
                Type = "Information",
                Message = "New test message"
            };

            // Act
            context.LogEntries.Add(newLogEntry);
            context.SaveChanges();

            // Assert
            var retrievedLogEntry = context.LogEntries.Find(newLogEntry.Id);
            Assert.That(retrievedLogEntry, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(retrievedLogEntry.DateTime, Is.EqualTo(newLogEntry.DateTime));
                Assert.That(retrievedLogEntry.Type, Is.EqualTo(newLogEntry.Type));
                Assert.That(retrievedLogEntry.Message, Is.EqualTo(newLogEntry.Message));
            });
        }
    }
}

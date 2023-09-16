using FizzBuzz.Api.Database.Models;

namespace FizzBuzz.Test.Database.Models
{
    [TestFixture]
    public class LogEntryTests
    {
        [Test]
        public void Id_Property_SetGet()
        {
            // Arrange
            var logEntry = new LogEntry();
            var expectedId = 123;

            // Act
            logEntry.Id = expectedId;

            // Assert
            Assert.That(logEntry.Id, Is.EqualTo(expectedId));
        }

        [Test]
        public void DateTime_Property_SetGet()
        {
            // Arrange
            var logEntry = new LogEntry();
            var expectedDateTime = DateTimeOffset.Now;

            // Act
            logEntry.DateTime = expectedDateTime;

            // Assert
            Assert.That(logEntry.DateTime, Is.EqualTo(expectedDateTime));
        }

        [Test]
        public void Type_Property_SetGet()
        {
            // Arrange
            var logEntry = new LogEntry();
            var expectedType = "Info";

            // Act
            logEntry.Type = expectedType;

            // Assert
            Assert.That(logEntry.Type, Is.EqualTo(expectedType));
        }

        [Test]
        public void Message_Property_SetGet()
        {
            // Arrange
            var logEntry = new LogEntry();
            var expectedMessage = "This is a log message.";

            // Act
            logEntry.Message = expectedMessage;

            // Assert
            Assert.That(logEntry.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void Type_Property_NullValue_SetGet()
        {
            // Arrange
            var logEntry = new LogEntry
            {
                // Act
                Type = null
            };

            // Assert
            Assert.That(logEntry.Type, Is.Null);
        }

        [Test]
        public void Message_Property_NullValue_SetGet()
        {
            // Arrange
            var logEntry = new LogEntry
            {
                // Act
                Message = null
            };

            // Assert
            Assert.That(logEntry.Message, Is.Null);
        }
    }
}

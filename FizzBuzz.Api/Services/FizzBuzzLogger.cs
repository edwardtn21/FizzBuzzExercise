using FizzBuzz.Api.Controllers;
using FizzBuzz.Api.Database;
using FizzBuzz.Api.Database.Models;
using FizzBuzz.Api.Interfaces;

namespace FizzBuzz.Api.Services
{
    public class FizzBuzzLogger : IFizzBuzzLogger
    {
        private readonly ILogger<FizzBuzzController> _logger;
        private readonly LoggerDbContext _loggerDbContext;

        public FizzBuzzLogger(ILogger<FizzBuzzController> logger, LoggerDbContext loggerDbContext) 
        {
            _logger = logger;
            _loggerDbContext = loggerDbContext;
        }

        public void Log(string message, string type = "Information")
        {
            CreateLogEntries(message, type);
        }
        
        public void LogError(string message)
        {
            CreateLogEntries(message, "Error");
        }

        public void LogInformation(string message)
        {
            CreateLogEntries(message);
        }

        private void CreateLogEntries(string message, string type = "Information")
        {
            DateTimeOffset dateTime = DateTimeOffset.UtcNow;
            LogEntry logEntry = new()
            {
                DateTime = dateTime,
                Type = type,
                Message = message
            };
            _loggerDbContext.LogEntries.Add(logEntry);
            _loggerDbContext.SaveChanges();
            switch (type)
            {
                case "Information":
                    _logger.LogInformation("{message}", message);
                    break;
                case "Error":
                    _logger.LogError("{message}", message);
                    break;
                default:
                    _logger.LogInformation("{message}", message);
                    break;
            }
        }
    }
}

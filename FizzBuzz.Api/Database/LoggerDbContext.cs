using FizzBuzz.Api.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace FizzBuzz.Api.Database
{
    public class LoggerDbContext : DbContext
    {
        public LoggerDbContext(DbContextOptions<LoggerDbContext> options)
        : base(options)
        {
        }
        
        public DbSet<LogEntry> LogEntries { get; set; }
    }
}

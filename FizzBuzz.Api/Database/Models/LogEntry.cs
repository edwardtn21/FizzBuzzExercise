using System.ComponentModel.DataAnnotations;

namespace FizzBuzz.Api.Database.Models
{
    public class LogEntry
    {
        [Key]
        public int Id { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public string? Type { get; set; }
        public string? Message { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FizzBuzz.Api.Models
{
    public class FizzBuzzRequest
    {
        [DefaultValue(100)] 
        public int? MaxNumber { get; set; }

    [DefaultValue("{\"3\":\"Fizz\",\"5\":\"Buzz\"}")]
    public Dictionary<int, string>? FizzBuzzValues { get; set; }
    }
}

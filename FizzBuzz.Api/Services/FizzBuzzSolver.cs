using FizzBuzz.Api.Interfaces;
using FizzBuzz.Api.Models;
using System.Text;

namespace FizzBuzz.Api.Services
{
    public class FizzBuzzSolver : IFizzBuzzSolver
    {
        public FizzBuzzResponse SolveFizzBuzz(FizzBuzzRequest fizzBuzzRequest)
        {
            FizzBuzzResponse fizzBuzzResponse = new()
            {
                FizzBuzzResults = new List<string>()
            };
            for (int i = 1; i <= fizzBuzzRequest.MaxNumber; i++)
            {
                fizzBuzzResponse.FizzBuzzResults.Add(CheckReplaceNumber(i, fizzBuzzRequest.FizzBuzzValues));
            }
            return fizzBuzzResponse;
        }

        private string CheckReplaceNumber(int number, Dictionary<int, string> replacementValues)
        {
            StringBuilder replacement = new();
            foreach (int multiple in replacementValues.Keys.ToList())
            {
                if (number % multiple == 0)
                {
                    replacement.Append(replacementValues[multiple]);
                }                
            }
            return string.IsNullOrEmpty(replacement.ToString()) ? number.ToString() : replacement.ToString();
        }

    }
}

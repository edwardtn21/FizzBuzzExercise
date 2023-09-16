using FizzBuzz.Api.Models;

namespace FizzBuzz.Api.Interfaces
{
    public interface IFizzBuzzSolver
    {
        public FizzBuzzResponse SolveFizzBuzz(FizzBuzzRequest fizzBuzzRequest);        
    }
}

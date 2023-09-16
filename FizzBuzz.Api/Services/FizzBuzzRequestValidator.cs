using FizzBuzz.Api.Models;
using FluentValidation;

namespace FizzBuzz.Api.Services
{
    public class FizzBuzzRequestValidator : AbstractValidator<FizzBuzzRequest>
    {
        public FizzBuzzRequestValidator()
        {
            RuleFor(x => x.MaxNumber).InclusiveBetween(0, 500);
            RuleFor(x => x.FizzBuzzValues).NotNull();
        }
    }
}

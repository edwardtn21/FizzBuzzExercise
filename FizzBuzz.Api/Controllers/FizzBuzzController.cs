using FizzBuzz.Api.Database;
using FizzBuzz.Api.Interfaces;
using FizzBuzz.Api.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FizzBuzz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FizzBuzzController : ControllerBase
    {
        private readonly IFizzBuzzLogger _logger;
        private readonly IValidator<FizzBuzzRequest> _validator;
        private readonly IFizzBuzzSolver _solver;
        
        public FizzBuzzController(IFizzBuzzLogger logger, IValidator<FizzBuzzRequest> validator, IFizzBuzzSolver solver)
        {
            _logger = logger;
            _validator = validator;
            _solver = solver;
        }

        [HttpPost(Name = "SolveFizzBuzz")]
        public async Task<IActionResult> SolveFizzBuzz([FromBody] FizzBuzzRequest fizzBuzzRequest)
        {
            ValidationResult result = await _validator.ValidateAsync(fizzBuzzRequest);
            if (!result.IsValid)
            {
                _logger.LogError("Invalid FizzBuzz Request: " + JsonSerializer.Serialize(fizzBuzzRequest));
                return new BadRequestResult();
            }
            _logger.LogInformation("Invalid FizzBuzz Request: " + JsonSerializer.Serialize(fizzBuzzRequest));
            FizzBuzzResponse fizzBuzzResponse;
            try
            {
                fizzBuzzResponse = _solver.SolveFizzBuzz(fizzBuzzRequest);
            } 
            catch (Exception ex)
            {
                _logger.LogError("Solve FizzBuzz Error: " + ex.Message);

                return new StatusCodeResult(500);
            }
            _logger.LogInformation("Solve FizzBuzz Response: " + JsonSerializer.Serialize(fizzBuzzResponse));                   
            return new OkObjectResult(fizzBuzzResponse);
        }
    }
}

using FizzBuzz.Api.Controllers;
using FizzBuzz.Api.Database;
using FizzBuzz.Api.Interfaces;
using FizzBuzz.Api.Models;
using FizzBuzz.Api.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace FizzBuzz.Api
{
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<LoggerDbContext>(opt =>
                opt.UseInMemoryDatabase("FizzBuzzLogs"));
            builder.Services.AddScoped<IFizzBuzzLogger, FizzBuzzLogger>(prov => new FizzBuzzLogger(
                prov.GetRequiredService<ILogger<FizzBuzzController>>(),
                prov.GetRequiredService<LoggerDbContext>()));
            builder.Services.AddScoped<IFizzBuzzSolver, FizzBuzzSolver>();
            builder.Services.AddScoped<IValidator<FizzBuzzRequest>, FizzBuzzRequestValidator>();

            WebApplication application = builder.Build();
            application.UseSwagger();
            application.UseSwaggerUI();
            application.UseHttpsRedirection();
            application.UseAuthorization();
            application.MapControllers();
            application.Run();
        }
    }
}
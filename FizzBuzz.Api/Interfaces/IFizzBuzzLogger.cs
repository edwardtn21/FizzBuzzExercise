namespace FizzBuzz.Api.Interfaces
{
    public interface IFizzBuzzLogger
    {
        public void Log(string message, string type = "Information");

        public void LogError(string message);

        public void LogInformation(string message);
    }
}

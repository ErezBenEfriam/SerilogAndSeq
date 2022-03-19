using Microsoft.AspNetCore.Mvc;

namespace SerilogAndSeq.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

       private readonly Serilog.ILogger _logger;
        public WeatherForecastController( Serilog.ILogger logger)
        {
            _logger = logger;
        }
        public readonly record struct DailyTemperature(double HighTemp, double LowTemp);
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var objectForLog = new { FirstName="dani",Age= 15};
            _logger.Information("Log List {Summaries}", Summaries); //log list
            _logger.Information("Log Object {@myobject}", objectForLog); //log object
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
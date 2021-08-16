using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emailservice;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Learn2CodeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IEmailSender _emailsender;

        public  WeatherForecastController(ILogger<WeatherForecastController> logger,IEmailSender emailsender)
        {
            _logger = logger;
            _emailsender = emailsender;
        }

        [HttpGet]
        public async Task< IEnumerable<WeatherForecast>> Get()
        {
            var rng = new Random();
            var message = new Message(new string[] { "" }, "hmmmm", "This is the content from our email.");
            await _emailsender.SendEmailAsync(message);
            
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}

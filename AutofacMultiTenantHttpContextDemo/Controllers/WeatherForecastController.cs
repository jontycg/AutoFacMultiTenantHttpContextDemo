using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AutofacMultiTenantHttpContextDemo.Controllers
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
        private readonly IGreeter _greeter;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IGreeter greeter)
        {
            _logger = logger;
            _greeter = greeter;
        }

        [HttpGet]
        public string Get()
        {
            return _greeter.SayGreeting();
        }
    }
}
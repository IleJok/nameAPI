using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nameAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NameController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<NameController> _logger;

        public NameController(ILogger<NameController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Name> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Name
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}

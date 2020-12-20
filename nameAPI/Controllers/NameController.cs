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
        NameContainer nameContainer;

        private readonly ILogger<NameController> _logger;
        List<Name> _namesList;

        public NameController(ILogger<NameController> logger)
        {
            _logger = logger;
             nameContainer = NameContainer.GetNameContainer();
            
        }

        private async Task getNames()
        {
            _namesList = await nameContainer.getNamesAsync();
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

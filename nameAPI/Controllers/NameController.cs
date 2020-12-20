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
        List<Name> _namesList = new List<Name>();

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
        public async Task<IEnumerable<Name>> GetAsync()
        {
            await getNames();
            return _namesList.ToArray();
        }
    }
}

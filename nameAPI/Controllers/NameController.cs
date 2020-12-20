using Microsoft.AspNetCore.Cors;
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
            if (_namesList.Count == 0)
            {
                _namesList = await nameContainer.getNamesAsync();
            }
            
        }

        /* returns the names in order, where the most common name is in top of the list*/
        [EnableCors("namefront")]
        [HttpGet]
        public async Task<IEnumerable<Name>> GetAsync()
        {
            await getNames();
            return _namesList.OrderByDescending(x => x.amount).ToArray();
        }

        /* returns the names in alphabetical order*/
        [EnableCors("namefront")]
        [HttpGet("alphabetical")]
        public async Task<IEnumerable<Name>> GetAlphabeticalAsync()
        {
            await getNames();
            return _namesList.OrderBy(x => x.name).ToArray();
        }

        /* returns the sum of different names*/
        [EnableCors("namefront")]
        [HttpGet("amount")]
        public async Task<int> GetAmountAsync()
        {
            await getNames();
            return _namesList.Sum(x => x.amount);
        }

        /* returns the amount for given name*/
        [EnableCors("namefront")]
        [HttpGet("count/{name}")]
        public async Task<int> GetNoAsync(string name)
        {
            string compare = name.Trim().ToLower();
            await getNames();
            var oneName = _namesList.Where(x => x.name.ToLower().Equals(compare)).FirstOrDefault();
            if (oneName == null)
            {
                return 0;
            }
            return oneName.amount;
        }
    }
}

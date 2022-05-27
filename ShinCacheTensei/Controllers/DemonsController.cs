using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShinCacheTensei.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemonsController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<int> GetSomething()
        {
            return Enumerable.Range(0, 5);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Runtime.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace ShinCacheTensei.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemonsController : ControllerBase
    {
        private readonly IMemoryCache MemoryCache;
        public DemonsController(IMemoryCache memoryCache) {
            //MemoryCache.CreateEntry(4).Value = "Valor";
            MemoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetSomething()
        {
            MemoryCache.TryGetValue(4, out var result);
            return Ok(Enumerable.Range(0, 5));
        }
    }
}
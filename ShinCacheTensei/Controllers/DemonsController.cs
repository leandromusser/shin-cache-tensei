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
        private readonly MemoryCache _MemoryCache;
        public DemonsController(MemoryCache memoryCache) {
            _MemoryCache.CreateEntry(4).Value = "Valor";
            _MemoryCache = memoryCache;
        }

        [HttpGet]
        public IEnumerable<int> GetSomething()
        {
            _MemoryCache.TryGetValue(4, out var result);
            return Enumerable.Range(0, 5);
        }
    }
}
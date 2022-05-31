using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Runtime.Caching;
using Microsoft.Extensions.Caching.Memory;
using ShinCacheTensei.Data.Repositories;
using ShinCacheTensei.Services;
using Microsoft.AspNetCore.Http;
using ShinCacheTensei.Entities;

namespace ShinCacheTensei.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemonsController : ControllerBase
    {
        private readonly IDemonService _demonService;
        public DemonsController(IDemonService demonService) {
            //MemoryCache.CreateEntry(4).Value = "Valor";
            _demonService = demonService;
        }

        [HttpGet]
        public IActionResult GetSomething()
        {
            //IDemonDtoTESTE<int> t = new DemonDtoTESTEIMPL1();
            //System.Convert.ChangeType(t, t.GetType());


            //MemoryCache.TryGetValue(4, out var result);
            //StatusCode(500);
            //return Ok(t);
            //return StatusCode(201, t);
            //return Problem("detalhe");
            return BadRequest("teste");
        }

        [HttpGet]
        [Route("testcache")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetFromCacheTestEndPoint(int id)
        {
            if (_demonService.TryGetDemonById(id, out object demon))
                return Ok(demon);

            //Só para testes
            _demonService.AddToCacheTemp(id);

            return NotFound();
        }
    }
}
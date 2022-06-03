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
using Swashbuckle.AspNetCore.Annotations;

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
        [Route("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "APENAS TESTE: Busca no cache por todos os demons com os ids passados. Caso não sejam encontrados lá, são criados e encontrados em " +
            "chamadas posteriores.")]
        public IActionResult GetFromCacheTestEndPoint([FromQuery(Name = "id")] int[] ids)
        {
            _demonService.GetById(ids[0], out Demon demon);
            return Ok(demon);
            if (_demonService.GetByIds(ids, out IEnumerable<Demon> demons))
                return Ok(demons);
            return NotFound();
        }
    }
}
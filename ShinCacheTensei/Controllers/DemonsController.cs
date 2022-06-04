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
using ShinCacheTensei.Data;

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
        [Route("searchC")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "APENAS TESTE: Busca no cache por todos os demons com os ids passados. Caso não sejam encontrados lá, são criados e encontrados em " +
            "chamadas posteriores.")]
        public IActionResult GetFromCacheTestEndPoint([FromQuery(Name = "id")] int[] ids)
        {
            _demonService.GetById(ids[0], out Demon demon);
            return Ok(demon);
            if (_demonService.GetByIds(ids, out IEnumerable<DemonDto> demonDtos))
                return Ok(demonDtos);
            return NotFound();
        }

        [HttpGet]
        [Route("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Retorna todas as informações dos Demons com os ids solicitados.")]
        public IActionResult GetDemons([FromQuery(Name = "id")] int[] ids)
        {
            if (_demonService.GetByIds(ids, out IEnumerable<DemonDto> demonDtos))
                return Ok(demonDtos);
            return NotFound();
        }


    }
}
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
            _demonService = demonService;
        }

        [HttpGet]
        [Route("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Retorna todas as informações dos Demons com os ids solicitados.")]
        public IActionResult GetDemons([FromQuery] DemonQueryParams demonQueryParams)
        {
            if (!this.ModelState.IsValid)
                return BadRequest("Invalid parameters!");

            if (_demonService.GetByIds(demonQueryParams.Ids, out IEnumerable<DemonDto> demonDtos))
                return Ok(demonDtos);
            return NotFound();
        }
    }
}
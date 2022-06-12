using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Runtime.Caching;
using Microsoft.Extensions.Caching.Memory;
using ShinCacheTensei.Data.Repositories;
using Microsoft.AspNetCore.Http;
using ShinCacheTensei.Entities;
using Swashbuckle.AspNetCore.Annotations;
using ShinCacheTensei.Data.Models;
using ShinCacheTensei.Dtos;
using ShinCacheTensei.Services;

namespace ShinCacheTensei.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FilterOptionsController : ControllerBase
    {
        private readonly IFilterOptionsService _filterOptionsService;
        public FilterOptionsController(IFilterOptionsService filterOptionsService) {
            _filterOptionsService = filterOptionsService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Retorna os filtros disponíveis a serem usados nas buscas por ids de Demons.")]
        public IActionResult GetAvailableFilters()
        {
            return Ok(_filterOptionsService.GetAvailableFilterOptions());
        }
        
    }
}
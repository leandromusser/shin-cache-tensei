﻿using Microsoft.AspNetCore.Mvc;
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
        [SwaggerOperation(Summary = "Retorna os ids dos Demons que estejam de acordo com os filtros selecionados.")]
        public async Task<IActionResult> GetDemonsIdsByFilters([FromQuery] DemonIdListQueryParams demonIdListQueryParams, [FromQuery(Name = "Quantity")] int quantity = 1)
        {
            var DemonIdListQueryParamsDto = await _demonService.GetIdsByFilters(demonIdListQueryParams, quantity);
            if (DemonIdListQueryParamsDto.DemonIds.Any())
                return Ok(DemonIdListQueryParamsDto);
            return NotFound();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Retorna todas as informações dos Demons com os ids solicitados.")]
        public async Task<IActionResult> GetDemonsByIds([FromQuery] DemonQueryParams demonQueryParams)
        {
            if (!this.ModelState.IsValid)
                return BadRequest();

            var demonDtos = await _demonService.GetByIds(demonQueryParams.Ids);
            if (demonDtos.Any())
                return Ok(demonDtos);
            return NotFound();
        }

    }
}
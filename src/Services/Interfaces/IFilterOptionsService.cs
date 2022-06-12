using ShinCacheTensei.Data.Models;
using ShinCacheTensei.Dtos;
using ShinCacheTensei.Entities;
using System;
using System.Collections.Generic;

namespace ShinCacheTensei.Services
{
    public interface IFilterOptionsService
    {
        public IEnumerable<FilterTotalOptionsDto> GetAvailableFilterOptions();
    }
}
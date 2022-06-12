using ShinCacheTensei.Data.Models;
using ShinCacheTensei.Dtos;
using ShinCacheTensei.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShinCacheTensei.Services
{
    public interface IDemonService
    {
        public Task<IEnumerable<DemonDto>> GetByIds(int[] ids);
        public Task<DemonIdListQueryParamsDto> GetIdsByFilters(DemonIdListQueryParams demonIdListQueryParams, int quantity);
    }
}
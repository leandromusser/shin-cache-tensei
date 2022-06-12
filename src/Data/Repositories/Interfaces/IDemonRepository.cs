using ShinCacheTensei.Data.Models;
using ShinCacheTensei.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShinCacheTensei.Data.Repositories
{
    public interface IDemonRepository
    {
        public Task<IEnumerable<Demon>> GetByIds(int[] ids);
        public Task<int[]> GetIdsByFilters(DemonIdListQueryParams demonIdListQueryParams, int quantity);
    }
}
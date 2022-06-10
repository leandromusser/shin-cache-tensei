using ShinCacheTensei.Data.Models;
using ShinCacheTensei.Entities;
using System.Collections.Generic;

namespace ShinCacheTensei.Data.Repositories
{
    public interface IDemonRepository
    {
        public bool GetByIds(int[] ids, out IEnumerable<Demon> demons);
        public bool GetIdsByFilters(DemonIdListQueryParams demonIdListQueryParams, int quantity, out int[] ids);
    }
}
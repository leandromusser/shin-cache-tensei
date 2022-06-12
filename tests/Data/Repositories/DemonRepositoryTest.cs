using ShinCacheTensei.Data.Models;
using ShinCacheTensei.Data.Repositories;
using ShinCacheTensei.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests.Data.Repositories
{
    internal class DemonRepositoryTest
    {
        public bool GetByIds(int[] ids, out IEnumerable<Demon> demons)
        {
            throw new NotImplementedException();
        }

        public bool GetIdsByFilters(DemonIdListQueryParams demonIdListQueryParams, int quantity, out int[] ids)
        {
            throw new NotImplementedException();
        }
    }
}

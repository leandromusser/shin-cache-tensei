using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using ShinCacheTensei.Entities;
using System.Linq;
using System.Collections.Generic;

namespace ShinCacheTensei.Data.Repositories
{
    public class DemonRepository : IDemonRepository
    {

        private readonly ShinCacheTenseiContext _shinCacheTenseiContext;
        public DemonRepository(ShinCacheTenseiContext shinCacheTenseiContext) {
            _shinCacheTenseiContext = shinCacheTenseiContext;
        }

        public bool GetById(int id, out Demon demon)
        {
            demon = _shinCacheTenseiContext.Demons.Where((d) => d.Id == id).FirstOrDefault();
            return demon == null;
        }

        public bool GetByIds(int[] ids, out IEnumerable<Demon> demons)
        {
            demons = _shinCacheTenseiContext.Demons.Where((demon) => ids.ToList().Contains(demon.Id)).AsEnumerable();
            return demons.Any();
        }
    }
}
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

            /*
            var d = new Demon();
            //d.Id = 5;
            d.Name = "Leandro";
            d.Level = 600;
            var demonContext = new DemonContext();
            demonContext.Demons.Add(d);
            demonContext.SaveChanges();
            */

            demon = _shinCacheTenseiContext.Demons.Where((d) => d.Id == id).FirstOrDefault();
            return demon == null;
        }

        public bool GetByIds(int[] ids, out IEnumerable<Demon> demons)
        {
            demons = _shinCacheTenseiContext.Demons.Where((d) => ids.ToList().Contains(d.Id)).AsEnumerable();
            return demons == null;
        }
    }
}
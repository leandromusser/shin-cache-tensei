using ShinCacheTensei.Entities;
using System.Collections.Generic;

namespace ShinCacheTensei.Data.Repositories
{
    public interface IDemonRepository
    {
        public bool GetById(int id, out Demon demon);
        public bool GetByIds(int[] ids, out IEnumerable<Demon> demons);
    }
}

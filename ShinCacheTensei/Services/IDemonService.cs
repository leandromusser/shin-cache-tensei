using ShinCacheTensei.Entities;
using System.Collections.Generic;

namespace ShinCacheTensei.Services
{
    public interface IDemonService
    {
        public void AddToCacheTemp(int id);
        public bool GetById(int id, out Demon demon);
        public bool GetByIds(int[] ids, out IEnumerable<Demon> demons);
    }
}
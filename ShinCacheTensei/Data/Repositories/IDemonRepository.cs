using ShinCacheTensei.Entities;

namespace ShinCacheTensei.Data.Repositories
{
    public interface IDemonRepository
    {
        public bool GetById(int id, out Demon demon);
    }
}

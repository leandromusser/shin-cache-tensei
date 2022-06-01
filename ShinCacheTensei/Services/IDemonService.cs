using ShinCacheTensei.Entities;

namespace ShinCacheTensei.Services
{
    public interface IDemonService
    {
        public void AddToCacheTemp(int id);

        public bool GetById(int id, out Demon demon);
    }
}
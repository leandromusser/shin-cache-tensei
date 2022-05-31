using ShinCacheTensei.Entities;

namespace ShinCacheTensei.Services
{
    public interface IDemonService
    {
        public void AddToCacheTemp(int id);

        public bool TryGetDemonById(int id, out object demon);
    }
}
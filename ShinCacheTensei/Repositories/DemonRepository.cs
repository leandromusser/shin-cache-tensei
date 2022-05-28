using Microsoft.Extensions.Caching.Memory;
using ShinCacheTensei.Services;

namespace ShinCacheTensei.Repositories
{
    public class DemonRepository: IDemonRepository
    {
        public readonly IDemonsCacheService DemonsCacheService;
        public DemonRepository(IDemonsCacheService demonsCacheService)
        {
            DemonsCacheService = demonsCacheService;
        }
    }
}

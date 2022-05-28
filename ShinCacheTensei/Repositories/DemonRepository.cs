using Microsoft.Extensions.Caching.Memory;
using ShinCacheTensei.Services;

namespace ShinCacheTensei.Repositories
{
    public class DemonRepository: IDemonRepository
    {
        public readonly DemonsCacheService DemonsCacheService;
        public DemonRepository(DemonsCacheService demonsCacheService)
        {
            DemonsCacheService = demonsCacheService;
        }
    }
}

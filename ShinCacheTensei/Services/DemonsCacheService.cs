using Microsoft.Extensions.Caching.Memory;

namespace ShinCacheTensei.Services
{
    public class DemonsCacheService
    {
        public readonly IMemoryCache MemoryCache;
        public DemonsCacheService(IMemoryCache memoryCache) {
            MemoryCache = memoryCache;
        }


    }
}

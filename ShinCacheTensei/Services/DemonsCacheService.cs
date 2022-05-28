using Microsoft.Extensions.Caching.Memory;
using ShinCacheTensei.Entities;

namespace ShinCacheTensei.Services
{
    public class DemonsCacheService: IDemonsCacheService
    {
        public readonly IMemoryCache MemoryCache;
        public DemonsCacheService(IMemoryCache memoryCache) {
            MemoryCache = memoryCache;
        }

        public bool TryObtainDemonFromCache(object key, out Demon demon) {
            return MemoryCache.TryGetValue(key, out demon);
        }
    }
}
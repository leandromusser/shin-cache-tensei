using Microsoft.Extensions.Caching.Memory;
using ShinCacheTensei.Entities;

namespace ShinCacheTensei.Data.Caching
{
    public class DemonCache : IDemonCache
    {
        public readonly IMemoryCache MemoryCache;
        public DemonCache(IMemoryCache memoryCache)
        {
            MemoryCache = memoryCache;
        }

        public bool TryObtainDemon(object key, out Demon demon)
        {
            MemoryCache.Set(key, key);
            
            //new MemoryCacheEntryOptions().


            return MemoryCache.TryGetValue(key, out demon);
        }
    }
}
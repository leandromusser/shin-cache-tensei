using Microsoft.Extensions.Caching.Memory;
using ShinCacheTensei.Entities;

namespace ShinCacheTensei.Data.Caching
{
    public interface IDemonCache
    {
        public bool TryObtainDemonFromCache(object key, out Demon demon);
    }
}
using Microsoft.Extensions.Caching.Memory;
using ShinCacheTensei.Entities;

namespace ShinCacheTensei.Data.Caching
{
    public interface IDemonCache
    {
        public bool TryObtainDemon(object key, out Demon demon);
    }
}
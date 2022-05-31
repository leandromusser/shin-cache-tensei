using Microsoft.Extensions.Caching.Memory;
using ShinCacheTensei.Entities;

namespace ShinCacheTensei.Data.Caching
{
    public interface ICacheHandler
    {

        public bool TryGetValue(object key, out object value);
        public void AddDurable(object key, object value);

    }
}
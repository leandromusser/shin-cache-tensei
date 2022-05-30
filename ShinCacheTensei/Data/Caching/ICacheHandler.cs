using Microsoft.Extensions.Caching.Memory;
using ShinCacheTensei.Entities;

namespace ShinCacheTensei.Data.Caching
{
    public interface ICacheHandler
    {

        //public TryObtainDemonWithName(object key, out Demon demon)
        public bool TryGetValue(object key, out object value);
        public bool Add(object key, object value);

    }
}
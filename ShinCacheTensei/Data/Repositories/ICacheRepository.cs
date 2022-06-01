using Microsoft.Extensions.Caching.Memory;
using ShinCacheTensei.Entities;

namespace ShinCacheTensei.Data.Repositories
{
    public interface ICacheRepository
    {

        public bool Get(object key, out object value);
        public void AddDurable(object key, object value);

    }
}
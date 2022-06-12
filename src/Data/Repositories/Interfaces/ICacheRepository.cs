using Microsoft.Extensions.Caching.Memory;
using ShinCacheTensei.Entities;
using System.Collections.Generic;

namespace ShinCacheTensei.Data.Repositories
{
    public interface ICacheRepository
    {

        public bool GetByKey(object key, out object value);
        public bool GetByKeys(object[] key, out IEnumerable<object> value);
        public void AddDurableValue(object key, object value);
        public void AddFastValue(object key, object value);

    }
}
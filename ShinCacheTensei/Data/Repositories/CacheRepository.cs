using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using ShinCacheTensei.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShinCacheTensei.Data.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        public readonly IMemoryCache _memoryCache;
        public readonly IConfiguration _configuration;

        public CacheRepository(IMemoryCache memoryCache, IConfiguration configuration)
        {
            _configuration = configuration;
            _memoryCache = memoryCache;
        }

        public bool GetByKey(object key, out object value)
        {
            return _memoryCache.TryGetValue(key, out value);
        }

        public bool GetByKeys(object[] keys, out IEnumerable<object> values) {

            values = new List<object>();
            var tempValue = values;

            keys.ToList().ForEach(k =>
            {
                if (_memoryCache.TryGetValue(k, out object tempObj))
                    tempValue.ToList().Add(tempObj);
            });
            values = tempValue;

            return values.Any();
        }

        public void AddDurable(object key, object value)
        {

            //Obter valor da duração do arquivo de configuração para não ficar algo Hard Coded
            var DurationInSecondsOfItemInDurableCache = _configuration.GetValue<double>("DurationInSecondsOfItemInDurableCache");
            using (var entry = _memoryCache.CreateEntry(key))
            {
                entry.Value = value;
                entry.SetValue(value);
                entry.SetAbsoluteExpiration(DateTimeOffset.UtcNow.AddSeconds(DurationInSecondsOfItemInDurableCache));
            }
        }
    }
    
}
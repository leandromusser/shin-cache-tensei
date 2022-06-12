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

        private readonly double _absoluteExpirationInSecondsOfDurableCacheValue;
        private readonly double _slidingExpirationInSecondsOfDurableCacheValue;
        private readonly double _absoluteExpirationInSecondsOfFastCacheValue;
        private readonly double _slidingExpirationInSecondsOfFastCacheValue;

        public CacheRepository(IMemoryCache memoryCache, IConfiguration configuration)
        {
            _configuration = configuration;
            _memoryCache = memoryCache;

            _absoluteExpirationInSecondsOfDurableCacheValue = _configuration.GetValue<double>("AbsoluteExpirationInSecondsOfDurableCacheValue");
            _slidingExpirationInSecondsOfDurableCacheValue = _configuration.GetValue<double>("SlidingExpirationInSecondsOfDurableCacheValue");
            _absoluteExpirationInSecondsOfFastCacheValue = _configuration.GetValue<double>("AbsoluteExpirationInSecondsOfFastCacheValue");
            _slidingExpirationInSecondsOfFastCacheValue = _configuration.GetValue<double>("SlidingExpirationInSecondsOfFastCacheValue");
        }

        private void AddValue(object key, object value, double absoluteExpiration, double slidingExpiration)
        {
            using (var entry = _memoryCache.CreateEntry(key))
            {
                entry.Value = value;
                entry.SetValue(value);
                entry.SetAbsoluteExpiration(DateTimeOffset.UtcNow.AddSeconds(absoluteExpiration));
                entry.SetSlidingExpiration(DateTimeOffset.UtcNow.AddSeconds(slidingExpiration) - DateTimeOffset.UtcNow);
            }
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

        public void AddDurableValue(object key, object value)
        {
            AddValue(key, value, _absoluteExpirationInSecondsOfDurableCacheValue, _slidingExpirationInSecondsOfDurableCacheValue);
        }

        public void AddFastValue(object key, object value)
        {
            AddValue(key, value, _absoluteExpirationInSecondsOfFastCacheValue, _slidingExpirationInSecondsOfFastCacheValue);
        }

    }
}
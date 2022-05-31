using ShinCacheTensei.Data.Caching;
using ShinCacheTensei.Data.Repositories;
using ShinCacheTensei.Entities;
using System;

namespace ShinCacheTensei.Services
{
    public class CacheableDemonService : IDemonService
    {
        public readonly ICacheHandler _cacheHandler;
        public readonly IDemonRepository _demonRepository;

        public CacheableDemonService(ICacheHandler cacheHandler, IDemonRepository demonRepository) {
            _cacheHandler = cacheHandler;
            _demonRepository = demonRepository;
        }

        public void AddToCacheTemp(int id)
        {
            var d = new Demon();
            d.Name = "Leandro";
            d.Race = "Warrior";
            d.Id = id;
            _cacheHandler.AddDurable(id, d);
        }

        public bool TryGetDemonById(int id, out object demon) {
            if (_cacheHandler.TryGetValue(id, out demon))
                return true;

            return false;
            //return _demonRepository.GetDemonById(id, out demon);
        }
    }
}
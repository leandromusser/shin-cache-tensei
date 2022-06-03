using ShinCacheTensei.Data;
using ShinCacheTensei.Data.Repositories;
using ShinCacheTensei.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShinCacheTensei.Services
{
    public class CacheableDemonService : IDemonService
    {
        public readonly ICacheRepository _cacheHandler;
        public readonly IDemonRepository _demonRepository;

        public CacheableDemonService(ICacheRepository cacheHandler, IDemonRepository demonRepository) {
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

        public void AddToCache(IEnumerable<Demon> demons) {
            demons.ToList().ForEach(d => _cacheHandler.AddDurable(d.Id, d));
        }

        public bool GetById(int id, out Demon demon) {/*
            if (_cacheHandler.GetByKey(id, out object obj_demon))
            {
                demon = (Demon) obj_demon;
                return true;
            }*/
            
            return _demonRepository.GetById(id, out demon);
        }

        public bool GetByIds(int[] ids, out IEnumerable<Demon> demons)
        {

            //Teste
            ids.ToList().ForEach(id =>
            {

                var d = new Demon();
                d.Name = "Leandro";
                d.Race = "Warrior";
                d.Id = id;
                _cacheHandler.AddDurable(id, d);

            });

            _demonRepository.GetByIds(ids, out demons);

            return demons.Any();
        }
    }
}
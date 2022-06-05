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
            var r = new DemonRace();
            r.Name = "Warrior";
            d.Race = r;
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

        public bool GetByIds(int[] ids, out IEnumerable<DemonDto> demonDtos)
        {
            //Tem que ter uma lógica de validação aqui

            var remainingDemonIds = new List<int>();
            demonDtos = new List<DemonDto>(){};

            foreach (int id in ids) {

                _cacheHandler.GetByKey(id, out object demon);
                if (demon == null)
                {
                    remainingDemonIds.Add(id);
                }
                else {
                    var demonDto = new DemonDto((Demon) demon, OriginType.ServerSideCache);
                    ((List<DemonDto>) demonDtos).Add(demonDto);
                }
            }
            if (remainingDemonIds.Count == 0)
                return true;


            if (_demonRepository.GetByIds(remainingDemonIds.ToArray(), out IEnumerable<Demon> demons)) {
                foreach (var demon in demons) {
                    var demonDto = new DemonDto(demon, OriginType.Database);
                    ((List<DemonDto>)demonDtos).Add(demonDto);
                    _cacheHandler.AddDurable(demon.Id, demon);
                }
            }

            return demonDtos.Any();
        }
    }
}
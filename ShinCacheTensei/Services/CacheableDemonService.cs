using ShinCacheTensei.Data.Models;
using ShinCacheTensei.Data.Repositories;
using ShinCacheTensei.Dtos;
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
        public readonly ICacheKeyGeneratorService _cacheKeyGeneratorService;


        /*
            DESENVOLVER O CONTROLLER DA PAGINAÇÃO

            AGORA OS DEMONS SÃO GUARDADOS COM AS SKILLS TAMBÉM, TUDO JUNTO. RECURSOS SEPARADOS PARA SKILLS PODERIA SER EM OUTRA VERSÃO, MAS...
            É DESNECESSÁRIO E TOMA MUITO TEMPO PARA ALGO QUE NÃO COMPENSA TANTO.

            PRECISA TAMBÉM DE UMA LISTA DINÂMICA COM LISTAGEM DOS NOMES DOS DEMONS E PAGINAÇÃO

            ADICIONAR JWT PARA EDIÇÃO DE DEMONS (EXCETO IMAGEM / IP FICA REGISTRADO NO BANCO DE DADOS)
            PODE SER CRIAÇÃO TAMBÉM, DESDE QUE SEJA POSSÍVEL O CARA FAZER REQUISIÇÃO DIRETO AO BD, ASSIM IGNORA O CACHE
            ESSE JWT DARÁ PODER ADMINISTRATIVO AO CARA PARA EDITAR OS DEMONS, AÍ ELES SERÃO AJUSTADOS NO BD E NO CACHE
            ROLE DO JWT: EDITOR

            VER O QUE ACONTECE SE DELETAR O DEMON DO BANCO (COMO FICA NO CACHE?)
            PARA CONSEGUIR A APIKEY: dar o nome, escolher se é recrutador, etc.

            A PÁGINA COM OS IDS DOS DEMONS NÃO PODE FICAR NO CACHE POR MUITO TEMPO, POIS CASO ALGUÉM ADICIONE UM DEMON E FAÇA A MESMA...
            ...PESQUISA, O NOVO DEMON NÃO VAI APARECER.
         */

        public CacheableDemonService(ICacheRepository cacheHandler, IDemonRepository demonRepository, ICacheKeyGeneratorService cacheKeyGeneratorService) {
            _cacheHandler = cacheHandler;
            _demonRepository = demonRepository;
            _cacheKeyGeneratorService = cacheKeyGeneratorService;
        }

        public bool GetByIds(int[] ids, out IEnumerable<DemonDto> demonDtos)
        {

            var remainingDemonIds = new List<int>();
            demonDtos = new List<DemonDto>(){};

            foreach (int id in ids) {

                _cacheHandler.GetByKey(_cacheKeyGeneratorService.GetDemonKey(id), out object demon);
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
                    _cacheHandler.AddDurableValue(_cacheKeyGeneratorService.GetDemonKey(demon.Id), demon);
                }
            }

            return demonDtos.Any();
        }

        public bool GetIdsByFilters(DemonIdListQueryParams demonIdListQueryParams, out int[] ids)
        {
            _demonRepository.GetIdsByFilters(demonIdListQueryParams, 5, 5, out ids);
            return true;
        }
    }
}
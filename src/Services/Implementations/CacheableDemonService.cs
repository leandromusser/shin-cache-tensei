﻿using Microsoft.Extensions.Configuration;
using ShinCacheTensei.Data.Models;
using ShinCacheTensei.Data.Repositories;
using ShinCacheTensei.Dtos;
using ShinCacheTensei.Entities;
using ShinCacheTensei.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShinCacheTensei.Services
{
    public class CacheableDemonService : IDemonService
    {
        public readonly ICacheRepository _cacheHandler;
        public readonly IDemonRepository _demonRepository;
        public readonly ICacheKeyGeneratorService _cacheKeyGeneratorService;
        public readonly IConfiguration _configuration;


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

        public CacheableDemonService(ICacheRepository cacheHandler, IDemonRepository demonRepository, ICacheKeyGeneratorService cacheKeyGeneratorService, IConfiguration configuration)
        {
            _cacheHandler = cacheHandler;
            _demonRepository = demonRepository;
            _cacheKeyGeneratorService = cacheKeyGeneratorService;
            _configuration = configuration;
        }

        public async Task<IEnumerable<DemonDto>> GetByIds(int[] ids)
        {

            var remainingDemonIds = new List<int>();
            var demonDtos = new List<DemonDto>(){};

            foreach (int id in ids) {

                _cacheHandler.GetByKey(_cacheKeyGeneratorService.GenerateDemonKey(id), out object demon);
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
                return demonDtos;

            foreach (var demon in await _demonRepository.GetByIds(remainingDemonIds.ToArray())) {
                var demonDto = new DemonDto(demon, OriginType.Database);
                ((List<DemonDto>)demonDtos).Add(demonDto);
                _cacheHandler.AddDurableValue(_cacheKeyGeneratorService.GenerateDemonKey(demon.Id), demon);
            }

            return demonDtos;
        }

        public async Task<DemonIdListQueryParamsDto> GetIdsByFilters(DemonIdListQueryParams demonIdListQueryParams, int quantity)
        {
            var ids = Array.Empty<int>();
            var demonIdListQueryParamsDto = new DemonIdListQueryParamsDto(ids, OriginType.None);

            if (quantity > _configuration.GetValue<int>("MaxReturnedDemonsIdsByFilters"))
                quantity = _configuration.GetValue<int>("MaxReturnedDemonsIdsByFilters");

            if (_cacheHandler.GetByKey(_cacheKeyGeneratorService.GenerateDemonIdListQueryParamsKey(demonIdListQueryParams.ToString(), quantity), out object cachedIds))
            {
                ids = (int[]) cachedIds;
                return new DemonIdListQueryParamsDto(ids, OriginType.ServerSideCache);
            }

            ids = await (_demonRepository.GetIdsByFilters(demonIdListQueryParams, quantity));
            if (ids.Any())
            {
                demonIdListQueryParamsDto = new DemonIdListQueryParamsDto(ids, OriginType.Database);
                _cacheHandler.AddDurableValue(_cacheKeyGeneratorService.GenerateDemonIdListQueryParamsKey(demonIdListQueryParams.ToString(), quantity), ids);
                return demonIdListQueryParamsDto;
            }

            demonIdListQueryParamsDto = new DemonIdListQueryParamsDto(ids, OriginType.None);
            _cacheHandler.AddFastValue(_cacheKeyGeneratorService.GenerateDemonIdListQueryParamsKey(demonIdListQueryParams.ToString(), quantity), ids);
            return demonIdListQueryParamsDto;
        }
    }
}
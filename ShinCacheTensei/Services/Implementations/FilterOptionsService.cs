using ShinCacheTensei.Data.Models;
using ShinCacheTensei.Data.Repositories;
using ShinCacheTensei.Dtos;
using ShinCacheTensei.Entities;
using ShinCacheTensei.Services.Enums;
using System;
using System.Collections.Generic;

namespace ShinCacheTensei.Services
{
    public class FilterOptionsService: IFilterOptionsService
    {

        public readonly ICacheRepository _cacheHandler;
        public readonly IFilterOptionRepository _filterOptionRepository;
        public readonly ICacheKeyGeneratorService _cacheKeyGeneratorService;

        public FilterOptionsService(ICacheRepository cacheHandler, IFilterOptionRepository filterOptionRepository, ICacheKeyGeneratorService cacheKeyGeneratorService)
        {
            _cacheHandler = cacheHandler;
            _filterOptionRepository = filterOptionRepository;
            _cacheKeyGeneratorService = cacheKeyGeneratorService;
        }

        private delegate IEnumerable<Tuple<int, string>> GetFiltersPerCategoryMethod();

        public IEnumerable<FilterTotalOptionsDto> GetAvailableFilterOptions() {
            
            //Refatorar este método para melhorar a legibilidade. 
            //Ex do que eu acho que está errado: Uso excessivo de "Item1", "Item2" em vários objetos, ficando confuso para saber o que é.
            //Ex do que eu acho que está errado: Duplicação da lógica do BD e do Cache (viola o DRY).
            //Além disso, preciso ver se Tuple é realmente a melhor opção.

            var filterOptionsDtos = new List<FilterOptionDto>();
            var filterTotalOptionsDtos = new List<FilterTotalOptionsDto>();

            new List<Tuple<string, GetFiltersPerCategoryMethod>> { 
                
                new Tuple<string, GetFiltersPerCategoryMethod>("Nature", _filterOptionRepository.GetNatureFilterOptions),
                new Tuple<string, GetFiltersPerCategoryMethod>("DemonRace", _filterOptionRepository.GetDemonRaceFilterOptions),
                new Tuple<string, GetFiltersPerCategoryMethod>("Skill", _filterOptionRepository.GetSkillFilterOptions)
            }
            .ForEach(categoryNameAndValues => {
                _cacheHandler.GetByKey(_cacheKeyGeneratorService.GenerateFilterKey(categoryNameAndValues.Item1), out object value);


                if (value == null)
                {

                    foreach (var tupleIdsAndValues in categoryNameAndValues.Item2.Invoke())
                    {
                        filterOptionsDtos.Add(new FilterOptionDto(tupleIdsAndValues.Item2, tupleIdsAndValues.Item1));
                    }
                    _cacheHandler.AddDurableValue(_cacheKeyGeneratorService.GenerateFilterKey(categoryNameAndValues.Item1), filterOptionsDtos);
                    filterTotalOptionsDtos.Add(new FilterTotalOptionsDto(filterOptionsDtos, categoryNameAndValues.Item1, OriginType.Database));
                    filterOptionsDtos = new List<FilterOptionDto>();

                }
                else {
                    foreach (var tupleIdsAndValues in (List<FilterOptionDto>) value) {
                        filterOptionsDtos.Add(new FilterOptionDto(tupleIdsAndValues.FilterValue, tupleIdsAndValues.FilterId));
                    }
                    filterTotalOptionsDtos.Add(new FilterTotalOptionsDto(filterOptionsDtos, categoryNameAndValues.Item1, OriginType.ServerSideCache));
                    filterOptionsDtos = new List<FilterOptionDto>();
                }
                
            });
            return filterTotalOptionsDtos;
        }
    }
}
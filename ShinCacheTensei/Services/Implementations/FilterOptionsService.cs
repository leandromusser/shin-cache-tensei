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

        private IEnumerable<FilterTotalOptionsDto> GetAvailableFilterOptions(Tuple<string, GetFiltersPerCategoryMethod>[] CategoryNameAndValues) {

            var filterOptionsDtos = new List<FilterOptionDto>();
            var filterTotalOptionsDtos = new List<FilterTotalOptionsDto>();

            foreach (var categoryNameAndValues in CategoryNameAndValues) {
                foreach (var tupleIdsAndValues in categoryNameAndValues.Item2.Invoke()) {
                    filterOptionsDtos.Add(new FilterOptionDto(tupleIdsAndValues.Item2, tupleIdsAndValues.Item1, OriginType.Database));
                }
                filterTotalOptionsDtos.Add(new FilterTotalOptionsDto(filterOptionsDtos, categoryNameAndValues.Item1));
                filterOptionsDtos = new List<FilterOptionDto>();
            }
            return filterTotalOptionsDtos;
        }

        public IEnumerable<FilterTotalOptionsDto> GetAvailableFilterOptions() {

            return GetAvailableFilterOptions(new List<Tuple<string, GetFiltersPerCategoryMethod>>() {
                Tuple.Create<string, GetFiltersPerCategoryMethod>("Nature", _filterOptionRepository.GetNatureFilterOptions),
                Tuple.Create<string, GetFiltersPerCategoryMethod>("DemonRace", _filterOptionRepository.GetDemonRaceFilterOptions),
                Tuple.Create<string, GetFiltersPerCategoryMethod>("Skill", _filterOptionRepository.GetSkillFilterOptions)
            }.ToArray());
        }
    }
}
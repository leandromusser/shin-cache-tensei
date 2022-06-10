using ShinCacheTensei.Data.Models;
using ShinCacheTensei.Data.Repositories;
using ShinCacheTensei.Dtos;
using ShinCacheTensei.Entities;
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

        public IEnumerable<FilterOptionDto> GetAvailableFilterOptions() {

            var filterOptionsDtos = new List<FilterOptionDto>();

            foreach (var t in _filterOptionRepository.GetNatureFilterOptions()) {
                filterOptionsDtos.Add(new FilterOptionDto("Nature", t.Item1, OriginType.Database));
            }

            return filterOptionsDtos;
        }
    }
}
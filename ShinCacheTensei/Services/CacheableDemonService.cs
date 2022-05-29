using ShinCacheTensei.Data.Caching;
using ShinCacheTensei.Data.Repositories;

namespace ShinCacheTensei.Services
{
    public class CacheableDemonService : IDemonService
    {
        public readonly IDemonCache _demonCache;
        public readonly IDemonRepository _demonRepository;

        public CacheableDemonService(IDemonCache demonCache, IDemonRepository demonRepository) {
            _demonCache = demonCache;
            _demonRepository = demonRepository;
        }
    }
}
using System.Security.Cryptography;
using System.Text;

namespace ShinCacheTensei.Services
{
    public sealed class CacheKeyGeneratorService: ICacheKeyGeneratorService
    {
        public string GenerateDemonKey(int id) => $"Demon: {id}";
        public string GenerateDemonIdListQueryParamsKey(string queryParams, int quantity) => $"QueryParams (max. {quantity}): {queryParams}";
        public string GenerateFilterKey(string filterName) => $"Filter: {filterName})";

    }
}
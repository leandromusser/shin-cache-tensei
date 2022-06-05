using System.Security.Cryptography;
using System.Text;

namespace ShinCacheTensei.Services
{
    public sealed class CacheKeyGeneratorService: ICacheKeyGeneratorService
    {
        public string GetDemonKey(int id) => $"Demon: {id}";

    }
}
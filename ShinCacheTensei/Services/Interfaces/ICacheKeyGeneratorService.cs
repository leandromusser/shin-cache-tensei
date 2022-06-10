using System.Security.Cryptography;
using System.Text;

namespace ShinCacheTensei.Services
{
    public interface ICacheKeyGeneratorService
    {
        public string GenerateDemonKey(int id);
        public string GenerateDemonIdListQueryParamsKey(string queryParams, int quantity);

    }
}
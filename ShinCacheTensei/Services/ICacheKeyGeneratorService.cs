using System.Security.Cryptography;
using System.Text;

namespace ShinCacheTensei.Services
{
    public interface ICacheKeyGeneratorService
    {
        public string GetDemonKey(int id);

    }
}
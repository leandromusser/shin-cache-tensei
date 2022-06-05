using ShinCacheTensei.Entities;
using ShinCacheTensei.Services;

namespace ShinCacheTensei.Dtos
{
    public class DemonDto
    {
        public DemonDto()
        { }

        public DemonDto(Demon demon, OriginType cameFrom)
        {
            Demon = demon;
            CameFrom = cameFrom;
        }

        public Demon Demon { get; set; }
        public OriginType CameFrom { get; set; }
    }
}

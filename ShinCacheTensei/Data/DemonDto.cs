using ShinCacheTensei.Entities;

namespace ShinCacheTensei.Data
{
    public class DemonDto
    {
        public DemonDto()
        {}

        public DemonDto(Demon demon, OriginType cameFrom)
        {
            Demon = demon;
            CameFrom = cameFrom;
        }

        public Demon Demon { get; set; }
        public OriginType CameFrom { get; set; }
    }
}

using ShinCacheTensei.Data.Models;
using ShinCacheTensei.Entities;
using ShinCacheTensei.Services;

namespace ShinCacheTensei.Dtos
{
    public class DemonIdListQueryParamsDto
    {
        public DemonIdListQueryParamsDto(){ }

        public DemonIdListQueryParamsDto(int[] demonIds, OriginType cameFrom)
        {
            DemonIds = demonIds;
            CameFrom = cameFrom;
        }

        public int[] DemonIds { get; set; }
        public OriginType CameFrom { get; set; }
    }
}

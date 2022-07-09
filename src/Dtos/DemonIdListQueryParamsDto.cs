using ShinCacheTensei.Data.Models;
using ShinCacheTensei.Entities;
using ShinCacheTensei.Services.Enums;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("demonIds")]
        public int[] DemonIds { get; set; }
        [JsonPropertyName("cameFrom")]
        public OriginType CameFrom { get; set; }
    }
}

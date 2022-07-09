using ShinCacheTensei.Entities;
using ShinCacheTensei.Services.Enums;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("demon")]
        public Demon Demon { get; set; }
        [JsonPropertyName("cameFrom")]
        public OriginType CameFrom { get; set; }
    }
}

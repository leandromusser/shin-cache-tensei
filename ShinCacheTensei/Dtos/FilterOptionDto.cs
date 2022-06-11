using ShinCacheTensei.Entities;
using ShinCacheTensei.Services.Enums;
using System;
using System.Text.Json.Serialization;

namespace ShinCacheTensei.Dtos
{
    public class FilterOptionDto
    {
        [JsonPropertyName("Id")]
        public int FilterId { get; set; }

        [JsonPropertyName("Value")]
        public string FilterValue { get; set; }
        public OriginType CameFrom { get; set; }

        public FilterOptionDto(string filterValue, int filterId, OriginType cameFrom) {
            FilterValue = filterValue;
            FilterId = filterId;
            CameFrom = cameFrom;
        }

    }
}
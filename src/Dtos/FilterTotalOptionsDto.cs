using ShinCacheTensei.Entities;
using ShinCacheTensei.Services.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ShinCacheTensei.Dtos
{
    public class FilterTotalOptionsDto
    {
        [JsonPropertyName("category")]
        public string FilterCategory { get; set; }
        [JsonPropertyName("cameFrom")]
        public OriginType CameFrom { get; set; }

        [JsonPropertyName("values")]
        public IEnumerable<FilterOptionDto> FilterOptionDtos { get; set; }

        public FilterTotalOptionsDto(IEnumerable<FilterOptionDto> filterOptionDtos, string filterCategory, OriginType cameFrom) {
            FilterOptionDtos = filterOptionDtos;
            FilterCategory = filterCategory;
            CameFrom = cameFrom;
        }

    }
}
using ShinCacheTensei.Entities;
using ShinCacheTensei.Services.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ShinCacheTensei.Dtos
{
    public class FilterTotalOptionsDto
    {
        [JsonPropertyName("Category")]
        public string FilterCategory { get; set; }

        [JsonPropertyName("Values")]
        public IEnumerable<FilterOptionDto> FilterOptionDtos { get; set; }

        public FilterTotalOptionsDto(IEnumerable<FilterOptionDto> filterOptionDtos, string filterCategory) {
            FilterOptionDtos = filterOptionDtos;
            FilterCategory = filterCategory;
        }

    }
}
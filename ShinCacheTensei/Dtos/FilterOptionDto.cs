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

        public FilterOptionDto(string filterValue, int filterId) {
            FilterValue = filterValue;
            FilterId = filterId;
        }

    }
}
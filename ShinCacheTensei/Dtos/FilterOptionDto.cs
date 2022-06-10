using ShinCacheTensei.Entities;
using ShinCacheTensei.Services;
using System;

namespace ShinCacheTensei.Dtos
{
    public class FilterOptionDto
    {
        public string FilterName { get; set; }
        public int FilterId { get; set; }
        public OriginType CameFrom { get; set; }

        public FilterOptionDto(string filterName, int filterId, OriginType cameFrom) {
            FilterName = filterName;
            FilterId = filterId;
            CameFrom = cameFrom;
        }

    }
}
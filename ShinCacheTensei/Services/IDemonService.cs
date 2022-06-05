using ShinCacheTensei.Dtos;
using ShinCacheTensei.Entities;
using System.Collections.Generic;

namespace ShinCacheTensei.Services
{
    public interface IDemonService
    {
        public bool GetByIds(int[] ids, out IEnumerable<DemonDto> demonDtos);
    }
}
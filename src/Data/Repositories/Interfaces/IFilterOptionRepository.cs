using ShinCacheTensei.Data.Models;
using ShinCacheTensei.Entities;
using System;
using System.Collections.Generic;

namespace ShinCacheTensei.Data.Repositories
{
    public interface IFilterOptionRepository
    {
        public IEnumerable<Tuple<int, string>> GetNatureFilterOptions();
        public IEnumerable<Tuple<int, string>> GetDemonRaceFilterOptions();
        public IEnumerable<Tuple<int, string>> GetSkillFilterOptions();
    }
}
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using ShinCacheTensei.Entities;
using System.Linq;
using System.Collections.Generic;
using ShinCacheTensei.Data.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace ShinCacheTensei.Data.Repositories
{ 
    public class FilterOptionRepository : IFilterOptionRepository
    {

        private readonly ShinCacheTenseiContext _shinCacheTenseiContext;

        public FilterOptionRepository(ShinCacheTenseiContext shinCacheTenseiContext) {
            _shinCacheTenseiContext = shinCacheTenseiContext;
        }

        public IEnumerable<Tuple<int, string>> GetNatureFilterOptions()
        {
            return _shinCacheTenseiContext.Natures.Select(n => new Tuple<int, string>(n.Id, n.Name));
        }
    }
}
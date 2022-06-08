using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using ShinCacheTensei.Entities;
using System.Linq;
using System.Collections.Generic;
using ShinCacheTensei.Data.Models;
using Microsoft.Extensions.Configuration;

namespace ShinCacheTensei.Data.Repositories
{ 
    public class DemonRepository : IDemonRepository
    {

        private readonly ShinCacheTenseiContext _shinCacheTenseiContext;
        public readonly IConfiguration _configuration;

        public DemonRepository(ShinCacheTenseiContext shinCacheTenseiContext, IConfiguration configuration) {
            _shinCacheTenseiContext = shinCacheTenseiContext;
            _configuration = configuration;
        }

        public bool GetIdsByFilters(DemonIdListQueryParams demonIdListQueryParams, int quantity, out int[] ids)
        {

            IQueryable<Demon> queryIdsss =
                from Demon in _shinCacheTenseiContext.Demons.Include(d => d.DemonInitialSkills)
                    .Include(d => d.DemonAffinities).Include(d => d.DemonAffinities).Include(d => d.Race)
                select Demon;

            if (demonIdListQueryParams.ResistNatureId != null)
                queryIdsss = queryIdsss.Where(d => d.DemonAffinities.Any(a => a.AffinityType.Name.Equals("Weak") && a.NatureId == demonIdListQueryParams.ResistNatureId));

            if (demonIdListQueryParams.WeakNatureId != null)
                queryIdsss = queryIdsss.Where(d => d.DemonAffinities.Any(a => !a.AffinityType.Name.Equals("Weak") && a.NatureId == demonIdListQueryParams.WeakNatureId));

            if (demonIdListQueryParams.MinimumLevel != null)
                queryIdsss = queryIdsss.Where(d => d.InitialLevel >= demonIdListQueryParams.MinimumLevel);

            if (demonIdListQueryParams.MaximumLevel != null)
                queryIdsss = queryIdsss.Where(d => d.InitialLevel <= demonIdListQueryParams.MaximumLevel);

            if (demonIdListQueryParams.DemonRaceId != null)
                queryIdsss = queryIdsss.Where(d => d.Race.Id == demonIdListQueryParams.DemonRaceId);

            if (demonIdListQueryParams.SkillId != null)
                queryIdsss = queryIdsss.Where(d => d.DemonInitialSkills.Any(s => s.SkillId == demonIdListQueryParams.SkillId));

            if (demonIdListQueryParams.ContainsThisTextInName != null)
                queryIdsss = queryIdsss.Where(d => d.Name.ToLower().Contains(demonIdListQueryParams.ContainsThisTextInName.ToLower()));

            if (demonIdListQueryParams.AfterId != null)
                queryIdsss = queryIdsss.Where(d => demonIdListQueryParams.AfterId <= d.Id);

            ids = queryIdsss.Select(d => d.Id).Take(quantity).ToArray();
            return ids.Any();

        }

        public bool GetById(int id, out Demon demon)
        {
            demon = _shinCacheTenseiContext.Demons.Where((d) => d.Id == id).FirstOrDefault();
            return demon == null;
        }

        public bool GetByIds(int[] ids, out IEnumerable<Demon> demons)
        {
            IQueryable<Demon> queryableDemons = _shinCacheTenseiContext.Demons.Include(p => p.DemonInitialSkills).ThenInclude(x => x.Skill).Include(x => x.DemonAffinities)
                .Where((demon) => ids.ToList().Contains(demon.Id));

            demons = queryableDemons.ToList();
            return demons.Any();
        }

    }
}
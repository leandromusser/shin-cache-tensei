using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using ShinCacheTensei.Entities;
using System.Linq;
using System.Collections.Generic;
using ShinCacheTensei.Data.Models;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

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

        public async Task<int[]> GetIdsByFilters(DemonIdListQueryParams demonIdListQueryParams, int quantity)
        {
            int[] ids;

            IQueryable<Demon> query =
                from Demon in _shinCacheTenseiContext.Demons.Include(d => d.DemonInitialSkills)
                    .Include(d => d.DemonAffinities).Include(d => d.Race)
                select Demon;

            if (demonIdListQueryParams.ResistNatureId != null)
                query = query.Where(d => d.DemonAffinities.Any(a => a.AffinityType.Name.Equals("Weak") && a.NatureId == demonIdListQueryParams.ResistNatureId));

            if (demonIdListQueryParams.WeakNatureId != null)
                query = query.Where(d => d.DemonAffinities.Any(a => !a.AffinityType.Name.Equals("Weak") && a.NatureId == demonIdListQueryParams.WeakNatureId));

            if (demonIdListQueryParams.MinimumLevel != null)
                query = query.Where(d => d.InitialLevel >= demonIdListQueryParams.MinimumLevel);

            if (demonIdListQueryParams.MaximumLevel != null)
                query = query.Where(d => d.InitialLevel <= demonIdListQueryParams.MaximumLevel);

            if (demonIdListQueryParams.DemonRaceId != null)
                query = query.Where(d => d.Race.Id == demonIdListQueryParams.DemonRaceId);

            if (demonIdListQueryParams.SkillId != null)
                query = query.Where(d => d.DemonInitialSkills.Any(s => s.SkillId == demonIdListQueryParams.SkillId));

            if (demonIdListQueryParams.ContainsThisTextInName != null)
                query = query.Where(d => d.Name.ToLower().Contains(demonIdListQueryParams.ContainsThisTextInName.ToLower()));

            if (demonIdListQueryParams.AfterId != null)
            {
                //Infelizmente, ao sair do IQueryable, todos os dados da query são puxados e filtrados aqui mesmo no servidor
                //Registro que tenho ciência deste problema
                ids = await query
                    .SkipWhile(s => s.Id != demonIdListQueryParams.AfterId)
                    .Skip(1).Take(quantity)
                    .Select(d => d.Id)
                    .ToArrayAsync();
            }
            else
                ids = await query.Take(quantity).Select(d => d.Id).ToArrayAsync();

            return ids;
        }

        public async Task<IEnumerable<Demon>> GetByIds(int[] ids)
        {
            IQueryable<Demon> queryableDemons = _shinCacheTenseiContext.Demons

                .Include(d => d.Race)
                .Include(d => d.DemonAffinities).ThenInclude(da => da.AffinityType)
                .Include(d => d.DemonAffinities).ThenInclude(da => da.Nature)
                .Include(d => d.RecruitingMethod)
                .Include(d => d.DemonInitialSkills).ThenInclude(dis => dis.Skill).ThenInclude(s => s.SkillType)

                .Where((demon) => ids.ToList().Contains(demon.Id));

            return await queryableDemons.ToListAsync();
        }

    }
}
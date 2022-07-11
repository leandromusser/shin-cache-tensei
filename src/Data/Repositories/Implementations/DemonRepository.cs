using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using ShinCacheTensei.Entities;
using System.Linq;
using System.Collections.Generic;
using ShinCacheTensei.Data.Models;
using System.Threading.Tasks;

namespace ShinCacheTensei.Data.Repositories
{ 
    public class DemonRepository : IDemonRepository
    {

        private readonly ShinCacheTenseiContext _shinCacheTenseiContext;

        public DemonRepository(ShinCacheTenseiContext shinCacheTenseiContext) {
            _shinCacheTenseiContext = shinCacheTenseiContext;
        }

        /*
            Busca todos os ids dos demons que estejam de acordo com os filtros passados.
            Exemplo: Quero os ids de (variável quantity) Demons que sejam da raça Divine e que venham depois do id x (paginação).
         */
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

            /*
                Lógica da paginação.
                Primeiro é verificado se o AfterId passado não é nulo, então as linhas são percorridas até chegar no Demon com esse id.
                Após isso, os (variável quantity) ids de Demons que estão após ele serão pegos.
             */
            if (demonIdListQueryParams.AfterId != null)
            {
                /*
                    Esta instrução possui um problema: Ao sair do IQueryable, todos os dados da query são puxados e filtrados aqui mesmo no...
                    ...servidor. Isso não é bom, pois causa processamento e transferência de dados adicionais.
                    Deixo registrado que tenho ciência disso.
                 */

                ids = await query.Select(d => d.Id).ToArrayAsync();
                ids = ids
                    .SkipWhile(s => s != demonIdListQueryParams.AfterId)
                    .Skip(1).Take(quantity)
                    .ToArray();
            }
            else
                //Pega os primeiros que aparecerem, caso o AfterId seja nulo
                ids = await query.Take(quantity).Select(d => d.Id).ToArrayAsync();

            return ids;
        }

        /*
            Método usado para buscar no banco de dados todos os demons possíveis que tenham os ids passados.
         */
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
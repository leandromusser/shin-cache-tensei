using ShinCacheTensei.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace ShinCacheTensei.Data
{
    public class ShinCacheTenseiContext: DbContext
    {
        public DbSet<AffinityType> AffinityTypes { get; set; }
        public DbSet<Demon> Demons { get; set; }
        public DbSet<DemonRace> DemonRaces { get; set; }
        public DbSet<DemonAffinity> DemonAffinities { get; set; }
        public DbSet<DemonInitialSkill> DemonInitialSkills { get; set; }
        public DbSet<Nature> Natures { get; set; }
        public DbSet<RecruitingMethod> RecruitingMethods { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SkillCost> SkillCosts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=Blogging;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DemonInitialSkill>().HasKey(l => new { l.DemonId, l.SkillId});
            modelBuilder.Entity<DemonAffinity>().HasKey(l => new { l.DemonId, l.NatureId, l.AffinityTypeId });

            var d = new Demon
            {
                Name = "Leandro",
                Id = 5,
                InitialLevel = 600
            };
            modelBuilder.Entity<Demon>().HasData(d);

        }
    }
}

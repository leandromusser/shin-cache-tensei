using ShinCacheTensei.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Collections.Generic;

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
        public DbSet<SkillType> SkillTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=Blogging;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DemonInitialSkill>().HasKey(l => new { l.DemonId, l.SkillId});
            modelBuilder.Entity<DemonAffinity>().HasKey(l => new { l.DemonId, l.NatureId, l.AffinityTypeId });

            modelBuilder.Entity<Demon>().OwnsOne(d => d.Race).HasData(new {DemonId = 5, Id = 5, Name = "Warrior"});
            modelBuilder.Entity<Demon>().OwnsOne(d => d.RecruitingMethod).HasData(new { DemonId = 5, Id = 5, Description = "Fusion only" });

            modelBuilder.Entity<Skill>().OwnsOne(s => s.SkillType).HasData(new { SkillId = 5, Id = 5, Type = "MP" });

            modelBuilder.Entity<DemonAffinity>().OwnsOne(da => da.AffinityType).HasData(new { DemonAffinityAffinityTypeId = 5, DemonAffinityDemonId = 5, DemonAffinityNatureId = 5, DemonAffinityId = 5, Id = 5, Type = "MP" });
            modelBuilder.Entity<DemonAffinity>().OwnsOne(da => da.Nature).HasData(new { DemonAffinityAffinityTypeId = 5, DemonAffinityDemonId = 5, DemonAffinityNatureId = 5, DemonAffinityId = 5, Id = 5, Name = "MP" });


            var d = new Demon
            {
                Name = "Leandro",
                Id = 5,
                InitialLevel = 50,
                
            };
            modelBuilder.Entity<Demon>().HasData(d);

            modelBuilder.Entity<DemonInitialSkill>().HasData(new { DemonId = 5, SkillId = 3, UnlockLevel = 54});

            
            //DemonInitialSkill[] dmi = { new DemonInitialSkill { SkillId = 3, Demon = d, DemonId = 5} };
            //d.DemonInitialSkills = dmi;


        }
    }
}

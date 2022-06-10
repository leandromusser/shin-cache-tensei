using ShinCacheTensei.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Collections.Generic;
using System.Linq;
using System.Data;

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

            /*
            modelBuilder.Entity<DemonRace>().HasMany(dr => dr.Demons).WithOne(d => d.Race).HasForeignKey(d => d.DemonRaceId);
            modelBuilder.Entity<RecruitingMethod>().HasMany(rm => rm.Demons).WithOne(d => d.RecruitingMethod).HasForeignKey(d => d.RecruitingMethodId);
            modelBuilder.Entity<SkillType>().HasMany(st => st.Skills).WithOne(s => s.SkillType).HasForeignKey(s => s.SkillTypeId);
            modelBuilder.Entity<Nature>().HasMany(n => n.DemonAffinities).WithOne(da => da.Nature).HasForeignKey(da => da.NatureId);
            modelBuilder.Entity<AffinityType>().HasMany(af => af.DemonAffinities).WithOne(da => da.AffinityType).HasForeignKey(da => da.AffinityTypeId);
            */


            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 3, Name = "Warrior" });
            modelBuilder.Entity<RecruitingMethod>().HasData(new RecruitingMethod { Id = 3, Description = "Fusion only" });
            modelBuilder.Entity<SkillType>().HasData(new SkillType { Id = 3, Type = "HP" });
            modelBuilder.Entity<Nature>().HasData(new Nature { Id = 5, Name = "Fire" });
            modelBuilder.Entity<AffinityType>().HasData(new AffinityType { Id = 5, Name = "Weak" });

            modelBuilder.Entity<Demon>().HasData(new Demon { Id = 5, Name = "Leandro", InitialLevel = 50, DemonRaceId = 3, RecruitingMethodId = 3 });
            modelBuilder.Entity<Demon>().HasData(new Demon { Id = 6, Name = "Ralph", InitialLevel = 65, DemonRaceId = 3, RecruitingMethodId = 3 });
            modelBuilder.Entity<Demon>().HasData(new Demon { Id = 7, Name = "Alex", InitialLevel = 31, DemonRaceId = 3, RecruitingMethodId = 3 });
            modelBuilder.Entity<Demon>().HasData(new Demon { Id = 8, Name = "Ana Carla", InitialLevel = 26, DemonRaceId = 3, RecruitingMethodId = 3 });
            modelBuilder.Entity<Demon>().HasData(new Demon { Id = 9, Name = "Striker", InitialLevel = 44, DemonRaceId = 3, RecruitingMethodId = 3 });
            modelBuilder.Entity<Demon>().HasData(new Demon { Id = 10, Name = "Rocker", InitialLevel = 92, DemonRaceId = 3, RecruitingMethodId = 3 });

            modelBuilder.Entity<Skill>().HasData(new Skill { Id = 5, Name = "Megido", SkillTypeId = 3, Description = "Deals 100 sacred damage to all foes", Cost = 80 });

            modelBuilder.Entity<DemonInitialSkill>().HasData(new DemonInitialSkill { DemonId = 5, SkillId = 5, UnlockLevel = 67});
            modelBuilder.Entity<DemonAffinity>().HasData(new DemonAffinity { DemonId = 5, NatureId = 5, AffinityTypeId = 5});

        }
    }
}

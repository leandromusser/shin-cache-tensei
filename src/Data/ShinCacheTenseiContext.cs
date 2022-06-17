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
        public ShinCacheTenseiContext(DbContextOptions dbContextOptions): base(dbContextOptions) {}

        public DbSet<AffinityType> AffinityTypes { get; set; }
        public DbSet<Demon> Demons { get; set; }
        public DbSet<DemonRace> DemonRaces { get; set; }
        public DbSet<DemonAffinity> DemonAffinities { get; set; }
        public DbSet<DemonInitialSkill> DemonInitialSkills { get; set; }
        public DbSet<Nature> Natures { get; set; }
        public DbSet<RecruitingMethod> RecruitingMethods { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SkillType> SkillTypes { get; set; }

        //Vou deixar vazio por enquanto para eu estudar mais sobre este método depois
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}

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


            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 1, Name = "Avatar" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 2, Name = "Avian" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 3, Name = "Beast" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 4, Name = "Brute" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 5, Name = "Deity" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 6, Name = "Divine" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 7, Name = "Dragon" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 8, Name = "Element" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 9, Name = "Entity" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 10, Name = "Fairy" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 11, Name = "Fallen" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 12, Name = "Femme" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 13, Name = "Fiend" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 14, Name = "Foul" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 15, Name = "Fury" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 16, Name = "Genma" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 17, Name = "Haunt" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 18, Name = "Holy" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 19, Name = "Jirae" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 20, Name = "Kishin" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 21, Name = "Lady" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 22, Name = "Megami" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 23, Name = "Mitama" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 24, Name = "Night" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 25, Name = "Raptor" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 26, Name = "Seraph" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 27, Name = "Snake" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 28, Name = "Tyrant" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 29, Name = "Vile" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 30, Name = "Wargod" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 31, Name = "Wilder" });
            modelBuilder.Entity<DemonRace>().HasData(new DemonRace { Id = 32, Name = "Yoma" });

            modelBuilder.Entity<RecruitingMethod>().HasData(new RecruitingMethod { Id = 1, Description = "Fusion only" });

            modelBuilder.Entity<SkillType>().HasData(new SkillType { Id = 1, Type = "HP" });
            modelBuilder.Entity<SkillType>().HasData(new SkillType { Id = 2, Type = "MP" });
            modelBuilder.Entity<SkillType>().HasData(new SkillType { Id = 3, Type = "Passive" });

            modelBuilder.Entity<Nature>().HasData(new Nature { Id = 1, Name = "Expel" });
            modelBuilder.Entity<Nature>().HasData(new Nature { Id = 2, Name = "Death" });
            modelBuilder.Entity<Nature>().HasData(new Nature { Id = 3, Name = "Mind" });
            modelBuilder.Entity<Nature>().HasData(new Nature { Id = 4, Name = "Curse" });
            modelBuilder.Entity<Nature>().HasData(new Nature { Id = 5, Name = "Nerve" });
            modelBuilder.Entity<Nature>().HasData(new Nature { Id = 6, Name = "Magic" });
            modelBuilder.Entity<Nature>().HasData(new Nature { Id = 7, Name = "Fire" });
            modelBuilder.Entity<Nature>().HasData(new Nature { Id = 8, Name = "Force" });
            modelBuilder.Entity<Nature>().HasData(new Nature { Id = 9, Name = "Ice" });
            modelBuilder.Entity<Nature>().HasData(new Nature { Id = 10, Name = "Phys" });
            modelBuilder.Entity<Nature>().HasData(new Nature { Id = 11, Name = "Elec" });
            modelBuilder.Entity<Nature>().HasData(new Nature { Id = 12, Name = "Ailments" });

            modelBuilder.Entity<AffinityType>().HasData(new AffinityType { Id = 1, Name = "Reflects" });
            modelBuilder.Entity<AffinityType>().HasData(new AffinityType { Id = 2, Name = "Resists" });
            modelBuilder.Entity<AffinityType>().HasData(new AffinityType { Id = 3, Name = "Absorbs" });
            modelBuilder.Entity<AffinityType>().HasData(new AffinityType { Id = 4, Name = "Void" });
            modelBuilder.Entity<AffinityType>().HasData(new AffinityType { Id = 5, Name = "Weak" });

            modelBuilder.Entity<Demon>().HasData(new Demon { Id = 5, Name = "Leandro", InitialLevel = 50, DemonRaceId = 1, RecruitingMethodId = 1 });
            modelBuilder.Entity<Demon>().HasData(new Demon { Id = 6, Name = "Ralph", InitialLevel = 65, DemonRaceId = 2, RecruitingMethodId = 1 });
            modelBuilder.Entity<Demon>().HasData(new Demon { Id = 7, Name = "Alex", InitialLevel = 31, DemonRaceId = 3, RecruitingMethodId = 1 });
            modelBuilder.Entity<Demon>().HasData(new Demon { Id = 8, Name = "Ana Carla", InitialLevel = 26, DemonRaceId = 4, RecruitingMethodId = 1 });
            modelBuilder.Entity<Demon>().HasData(new Demon { Id = 9, Name = "Striker", InitialLevel = 44, DemonRaceId = 5, RecruitingMethodId = 1 });
            modelBuilder.Entity<Demon>().HasData(new Demon { Id = 10, Name = "Rocker", InitialLevel = 92, DemonRaceId = 6, RecruitingMethodId = 1 });

            modelBuilder.Entity<Skill>().HasData(new Skill { Id = 5, Name = "Megido", SkillTypeId = 2, Description = "Deals 100 sacred damage to all foes", Cost = 80 });

            modelBuilder.Entity<DemonInitialSkill>().HasData(new DemonInitialSkill { DemonId = 5, SkillId = 5, UnlockLevel = 67});
            modelBuilder.Entity<DemonAffinity>().HasData(new DemonAffinity { DemonId = 5, NatureId = 5, AffinityTypeId = 5});

        }
    }
}

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



            modelBuilder.Entity<Demon>(d =>
            {
                d.HasData(new Demon
                {
                    Id = 6,
                    Name = "Leandroqqq",
                    InitialLevel = 50
                });
            });


            modelBuilder.Entity<Demon>(d =>
            {
                d.HasData(new Demon
                {
                    Id = 7,
                    Name = "Leandrovcxvcx",
                    InitialLevel = 50
                });
            });


            modelBuilder.Entity<Demon>(d =>
            {
                d.HasData(new Demon
                {
                    Id = 8,
                    Name = "Leandroxzaaaaa",
                    InitialLevel = 50
                });
            });


            modelBuilder.Entity<Demon>(d =>
            {
                d.HasData(new Demon
                {
                    Id = 9,
                    Name = "Leandrosaa",
                    InitialLevel = 50
                });
            });


            modelBuilder.Entity<Demon>(d =>
            {
                d.HasData(new Demon
                {
                    Id = 10,
                    Name = "Leandrocxz",
                    InitialLevel = 50
                });
            });



            //modelBuilder.Entity<Demon>().OwnsOne(d => d.Race).HasData(new {DemonId = 5, Id = 5, Name = "Warrior"});
            // modelBuilder.Entity<Demon>().OwnsOne(d => d.RecruitingMethod).HasData(new { DemonId = 5, Id = 5, Description = "Fusion only" });

            // modelBuilder.Entity<Skill>().OwnsOne(s => s.SkillType).HasData(new { SkillId = 5, Id = 5, Type = "MP" });

            //modelBuilder.Entity<DemonAffinity>().OwnsOne(da => da.AffinityType).HasData(new { DemonAffinityAffinityTypeId = 5, DemonAffinityDemonId = 5, DemonAffinityNatureId = 5, DemonAffinityId = 5, Id = 5, Type = "MP" });
            //modelBuilder.Entity<DemonAffinity>().OwnsOne(da => da.Nature).HasData(new { DemonAffinityAffinityTypeId = 5, DemonAffinityDemonId = 5, DemonAffinityNatureId = 5, DemonAffinityId = 5, Id = 5, Name = "MP" });

            modelBuilder.Entity<Skill>(d =>
            {
                d.HasData(new Skill
                {
                    Id = 5,
                    Name = "Megido",
                    Description = "Deals 100 sacred damage to all foes",
                    Cost = 80
                });
                d.OwnsOne(dw => dw.SkillType).HasData(new
                {
                    SkillId = 5,
                    Id = 3,
                    Type = "HP"
                });
            });

            modelBuilder.Entity<Demon>(d =>
            {
                d.HasData(new Demon
                {
                    Id = 5,
                    Name = "Leandro",
                    InitialLevel = 50
                });
                d.OwnsOne(dw => dw.RecruitingMethod).HasData(new
                {
                    DemonId = 5,
                    Id = 3,
                    Description = "Fusion only"
                });
                d.OwnsOne(dw => dw.Race).HasData(new
                {
                    DemonId = 5,
                    Id = 3,
                    Name = "Warrior"
                });
            });

            modelBuilder.Entity<DemonInitialSkill>(d =>
            {
                d.HasData(new DemonInitialSkill
                {
                    DemonId = 5,
                    SkillId = 5,
                    UnlockLevel = 67
                });

            });

            modelBuilder.Entity<DemonAffinity>(d =>
            {
                d.HasData(new DemonAffinity
                {
                    DemonId = 5,
                    NatureId = 5,
                    AffinityTypeId = 5
                });

                d.OwnsOne(dw => dw.Nature).HasData(new
                {
                    DemonAffinityDemonId = 5,
                    DemonAffinityNatureId = 5,
                    DemonAffinityAffinityTypeId = 5,
                    Id = 5,
                    Name = "Fire"
                });

                d.OwnsOne(dw => dw.AffinityType).HasData(new
                {
                    DemonAffinityDemonId = 5,
                    DemonAffinityNatureId = 5,
                    DemonAffinityAffinityTypeId = 5,
                    Id = 5,
                    Name = "Weak"
                });

            });

            //modelBuilder.Entity<DemonInitialSkill>().Ignore(a => a.Demon);




            //modelBuilder.Entity<DemonInitialSkill>().HasData(new { DemonId = 5, SkillId = 5, UnlockLevel = 54});


            //DemonInitialSkill[] dmi = { new DemonInitialSkill { SkillId = 3, Demon = d, DemonId = 5} };
            //d.DemonInitialSkills = dmi;


        }
    }
}

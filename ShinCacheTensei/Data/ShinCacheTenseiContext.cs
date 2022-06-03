using ShinCacheTensei.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace ShinCacheTensei.Data
{
    public class ShinCacheTenseiContext: DbContext
    {
        public DbSet<AffinityType> AffinityTypes { get; set; }
        public DbSet<Demon> Demons { get; set; }
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

            //modelBuilder.Entity<DemonAffinity>().HasKey(k => k.Demon.Id);

            //modelBuilder.Entity<DemonAffinity>().OwnsOne<Demon>(x => x.Demon);
            //modelBuilder.Entity<DemonInitialSkill>().OwnsOne<Demon>(x => x.Demon).HasKey()

            //modelBuilder.Entity<Demon>().Property(p => p.DemonInitialSkills).

            //modelBuilder.Entity<Demon>().HasOne<Demon>(x => x).WithMany<DemonAffinity>(x => x.STR);

            //modelBuilder.Entity<DemonInitialSkill>().HasKey(k => k.Demon.Id);

            //modelBuilder.Entity<DemonAffinity>().HasOne<Demon>(x => x.Demon).WithMany(x => x.Id);

            //modelBuilder.Entity<DemonAffinity>().HasKey(x => x.DemonId).

            //Pesquisar por Value Object / Objeto de valor
            //O Entity Framework não suporta tipos complexos, por isso vai dar erro aqui com o k.Demon.Id. Teria que ser tipo um K.DemonId
            //modelBuilder.Entity<DemonInitialSkill>().HasKey(k => k.Demon.Id);
        }
    }
}

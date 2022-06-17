using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using ShinCacheTensei.Data;
using ShinCacheTensei.Data.Models;
using ShinCacheTensei.Data.Repositories;
using ShinCacheTensei.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests.Data.Repositories
{
    [TestFixture]
    public class DemonRepositoryTest
    {
        //Trocar o nome do banco de dados depois para tirar esse que é padrão
        private readonly DbContextOptions<ShinCacheTenseiContext> dbContextOptions = new DbContextOptionsBuilder<ShinCacheTenseiContext>()
        .UseInMemoryDatabase(databaseName: "Blogging")
        .Options;
        private ShinCacheTenseiContext _shinCacheTenseiContext;
        private DemonRepository _demonRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            /*
            //Vai ser usados nos testes do CacheRepository
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath("/")
                .AddInMemoryCollection(new Dictionary<string, string>())
                .Build();
            */

            _shinCacheTenseiContext = new ShinCacheTenseiContext(dbContextOptions);
            AddData();
            _demonRepository = new DemonRepository(_shinCacheTenseiContext);
        }

        private void AddData() {
            _shinCacheTenseiContext.RecruitingMethods.Add(new RecruitingMethod() { Id = 1, Description = "Fusion only" });
            _shinCacheTenseiContext.DemonRaces.Add(new DemonRace() { Id = 1, Name = "Avatar" });
            _shinCacheTenseiContext.Demons.Add(new Demon() { Id = 5, Name = "Leandro", InitialLevel = 50, DemonRaceId = 1, RecruitingMethodId = 1});
            _shinCacheTenseiContext.SaveChanges();
        }

        [Test]
        public void FirstTest() {
            Assert.That(_demonRepository.GetByIds(new int[] {5}).Result, Has.Count.AtLeast(1));
        }
        
    }
}

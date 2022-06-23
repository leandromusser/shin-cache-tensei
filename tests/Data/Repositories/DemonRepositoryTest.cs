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
        private readonly DbContextOptions<ShinCacheTenseiContext> dbContextOptions = new DbContextOptionsBuilder<ShinCacheTenseiContext>()
        .UseInMemoryDatabase(databaseName: "ShinCacheTensei")
        .Options;
        private ShinCacheTenseiContext _shinCacheTenseiContext;
        private DemonRepository _demonRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            _shinCacheTenseiContext = new ShinCacheTenseiContext(dbContextOptions);
            AddData();
            _demonRepository = new DemonRepository(_shinCacheTenseiContext);
        }

        private void AddData() {
            _shinCacheTenseiContext.RecruitingMethods.Add(new RecruitingMethod() { Id = 1, Description = "Fusion only" });
            _shinCacheTenseiContext.DemonRaces.Add(new DemonRace() { Id = 1, Name = "Avatar" });
            _shinCacheTenseiContext.Demons.Add(new Demon() { Id = 5, Name = "Leandro", InitialLevel = 50, DemonRaceId = 1, RecruitingMethodId = 1 });
            _shinCacheTenseiContext.Demons.Add(new Demon() { Id = 7, Name = "Starlight", InitialLevel = 51, DemonRaceId = 1, RecruitingMethodId = 1 });
            _shinCacheTenseiContext.Demons.Add(new Demon() { Id = 8, Name = "Nea", InitialLevel = 62, DemonRaceId = 1, RecruitingMethodId = 1 });
            _shinCacheTenseiContext.SaveChanges();
        }

        [Test]
        [TestCase(new int[] { 5, 7, 8, 9 })]
        [TestCase(new int[] { 9 })]
        public void ShouldReturnXDemonsMinusNonExistents(int[] demonIdsAndNonExistentDemonId)
        {
            Assert.That(_demonRepository.GetByIds(demonIdsAndNonExistentDemonId).Result, Has.Count.EqualTo(demonIdsAndNonExistentDemonId.Length - 1));
        }

        [Test]
        public void ShouldReturnTwoDemonIdsUsingFilters()
        {
            var demonIdListQueryParams = new DemonIdListQueryParams
            {
                MinimumLevel = 51,
                DemonRaceId = 1,
                ContainsThisTextInName = "a"
            };

            Assert.That(_demonRepository.GetIdsByFilters(demonIdListQueryParams, 5).Result, Has.Length.EqualTo(2));
        }
    }
}

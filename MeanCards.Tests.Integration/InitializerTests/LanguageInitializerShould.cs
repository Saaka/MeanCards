﻿using MeanCards.DAL.Interfaces.Initializer;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Tests.Integration.Config;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.InitializerTests
{
    public class LanguageInitializerShould : IClassFixture<DALServiceCollectionFixture>
    {
        private readonly DALServiceCollectionFixture fixture;

        public LanguageInitializerShould(DALServiceCollectionFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task InsertTwoBaseLanguages()
        {
            var languageInitializer = fixture.GetService<ILanguageInitializer>();
            await languageInitializer.Seed();

            var languageRepository = fixture.GetService<ILanguagesRepository>();
            var languages = await languageRepository.GetAllActiveLanguages();

            Assert.Contains(languages, x => x.Code == "PL");
            Assert.Contains(languages, x => x.Code == "EN");
        }
    }
}

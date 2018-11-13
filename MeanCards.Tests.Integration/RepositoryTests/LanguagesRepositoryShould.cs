using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Tests.Integration.Config;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.RepositoryTests
{
    public class LanguagesRepositoryShould : IDisposable
    {
        private readonly DALServiceCollectionFixture Fixture;

        public LanguagesRepositoryShould()
        {
            Fixture = new DALServiceCollectionFixture();
        }

        [Fact]
        public async Task StoreDataInDatabase()
        {
            var repository = Fixture.GetService<ILanguagesRepository>();

            await repository.CreateLanguage(new Model.Creation.CreateLanguageModel { Code = "PL", Name = "Polski" });
            var languages = await repository.GetAllActiveLanguages();

            Assert.Single(languages);

            var language = languages.First();
            Assert.Equal("PL", language.Code);
            Assert.Equal("Polski", language.Name);
            Assert.True(language.IsActive);
            Assert.NotEqual(0, language.LanguageId);
        }

        public void Dispose()
        {
            Fixture.Dispose();
        }
    }
}

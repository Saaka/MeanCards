using MeanCards.DAL.Interfaces.Repository;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Core
{
    public class CoreServiceCollectionFixtureShould : BaseCoreTests
    {
        [Fact]
        public async Task ExecuteDatabaseInitialization()
        {
            var repository = Fixture.GetService<ILanguagesRepository>();

            var languages = await repository.GetAllActiveLanguages();

            Assert.NotEmpty(languages);
        }
    }
}

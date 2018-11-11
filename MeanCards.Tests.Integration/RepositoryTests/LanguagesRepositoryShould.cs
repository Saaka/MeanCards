using MeanCards.DAL.Repository;
using MeanCards.DAL.Storage;
using MeanCards.Tests.Integration.Helpers;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.RepositoryTests
{
    public class LanguagesRepositoryShould
    {
        [Fact]
        public async Task StoreDataInDatabase()
        {
            var options = TestInMemoryDbOptionsProvider.CreateOptions<AppDbContext>();

            using (var context = new AppDbContext(options))
            {
                var repository = new LanguagesRepository(context);
                await repository.CreateLanguage(new Model.Creation.CreateLanguageModel { Code = "PL", Name = "Polski " });
            }
            
            using (var context = new AppDbContext(options))
            {
                var repository = new LanguagesRepository(context);
                var languages = await repository.GetAllActiveLanguages();

                Assert.Single(languages);
            }
        }
    }
}

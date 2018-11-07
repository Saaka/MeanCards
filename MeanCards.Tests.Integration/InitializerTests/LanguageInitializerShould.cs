using MeanCards.DAL.Initializer;
using MeanCards.DAL.Storage;
using MeanCards.Tests.Integration.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.InitializerTests
{
    public class LanguageInitializerShould
    {
        [Fact]
        public async Task InsertTwoBaseLanguages()
        {
            var options = TestInMemoryDbOptionsProvider.CreateOptions<AppDbContext>();

            using (var context = new AppDbContext(options))
            {
                var initializer = new LanguageInitializer(context);
                await initializer.Seed();
            }

            using (var context = new AppDbContext(options))
            {
                var languages = await context.Languages.ToListAsync();

                Assert.Contains(languages, x => x.Code == "PL");
                Assert.Contains(languages, x => x.Code == "EN");
            }
        }
    }
}

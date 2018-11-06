using Microsoft.EntityFrameworkCore;

namespace MeanCards.Tests.Integration.Helpers
{
    public class TestInMemoryDbOptionsProvider
    {
        public static DbContextOptions<T> CreateOptions<T>(string databaseName = "test_db")
            where T : DbContext
        {
            var options = new DbContextOptionsBuilder<T>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            return options;
        }
    }
}

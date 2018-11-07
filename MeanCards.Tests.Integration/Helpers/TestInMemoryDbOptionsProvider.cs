using Microsoft.EntityFrameworkCore;
using System;

namespace MeanCards.Tests.Integration.Helpers
{
    public class TestInMemoryDbOptionsProvider
    {
        public static DbContextOptions<T> CreateOptions<T>(string databaseName = null)
            where T : DbContext
        {
            if (string.IsNullOrEmpty(databaseName))
                databaseName = Guid.NewGuid().ToString();

            var options = new DbContextOptionsBuilder<T>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            return options;
        }
    }
}

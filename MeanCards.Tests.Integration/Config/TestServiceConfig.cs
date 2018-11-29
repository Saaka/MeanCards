using MeanCards.DAL.Interfaces.Transactions;
using MeanCards.DAL.Storage;
using MeanCards.Tests.Integration.BaseTests.Transactions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MeanCards.Tests.Integration.Config
{
    public static class TestServiceConfig
    {
        [Obsolete("Use RegisterSQLiteInmemoryContext to use unique constraints etc")]
        public static IServiceCollection RegisterInmemoryContext(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            return services;
        }

        public static IServiceCollection RegisterSQLiteInmemoryContext(this IServiceCollection services, SqliteConnection connection)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(connection));
            EnsureContextIsCreated(connection);

            //Mock transaction, TransactionScope is currently unavailable for SqliteConnection
            services
                .AddTransient<IRepositoryTransactionsFactory, SqliteMockTransactionScopeFactory>();

            return services;
        }

        private static void EnsureContextIsCreated(SqliteConnection connection)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(connection)
                .Options;
            
            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureCreated();
            }
        }
    }
}

using MeanCards.DAL.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MeanCards.DAL.Transaction;
using MeanCards.DAL.Interfaces.Transactions;
using System;

namespace MeanCards.Tests.Core.Config
{
    public static class CoreTestsConfiguration
    {
        public const string DefaultTestDatabase = "MeanCards_Tests";
        public const string ConnectionStringConfigProperty = "DbSettings:ConnectionString";
        public const string DatabasePlaceholderConfigProperty = "DbSettings:DatabaseNamePlaceholder";

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }

        public static IServiceCollection RegisterCoreTestsContext(this IServiceCollection services, IConfiguration configuration, string databaseName = DefaultTestDatabase)
        {
            services.AddDbContext<AppDbContext>((opt) =>
            opt.UseSqlServer(
                GetConnectionString(configuration, databaseName),
                cb =>
                {
                    cb.MigrationsHistoryTable(AppDbContext.DefaultMigrationsTable);
                }),
            ServiceLifetime.Scoped);

            services
                .AddTransient<IRepositoryTransactionsFactory, DbContextRepositoryTransactionsFactory>();

            return services;
        }

        public static string GetConnectionString(IConfiguration configuration, string databaseName)
        {
            var connectionString = configuration[ConnectionStringConfigProperty];
            var databasePlaceholder = configuration[DatabasePlaceholderConfigProperty];

            if (!string.IsNullOrWhiteSpace(connectionString) && !string.IsNullOrWhiteSpace(databasePlaceholder))
            {
                var createdConnection = connectionString.Replace(databasePlaceholder, databaseName);

                return createdConnection;
            }

            throw new ArgumentException("Missing tests database configuration");
        }
    }
}

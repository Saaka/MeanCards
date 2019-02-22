using MeanCards.DAL.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MeanCards.DAL.Transaction;
using MeanCards.DAL.Interfaces.Transactions;
using System;
using MeanCards.UserManagement;
using MeanCards.GameManagement;
using MeanCards.Queries.GameQueries;

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

        public static IServiceCollection RegisterLegacyHandlerInterfaces(this IServiceCollection services)
        {
            services
                //Handlers
                .AddScoped<ICreateUserHandler, CreateUserHandler>()
                .AddScoped<IAuthenticateGoogleUserHandler, AuthenticateGoogleUserHandler>()
                .AddScoped<ICreateGameHandler, CreateGameHandler>()
                .AddScoped<IJoinGameHandler, JoinGameHandler>()
                .AddScoped<IStartGameRoundHandler, StartGameRoundHandler>()
                .AddScoped<ISubmitAnswerHandler, SubmitAnswerHandler>()
                .AddScoped<IEndSubmissionsHandler, EndSubmissionsHandler>()
                .AddScoped<ISkipRoundHandler, SkipRoundHandler>()
                .AddScoped<ICancelGameHandler, CancelGameHandler>()
                .AddScoped<ISelectAnswerHandler, SelectAnswerHandler>()

                //query handlers
                .AddScoped<IGetUserByCredentialsHandler, GetUserByCredentialsHandler>()
                .AddScoped<IGetUserByCodeHandler, GetUserByCodeHandler>()
                .AddScoped<IGetGameListQueryHandler, GetGameListQueryHandler>();

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

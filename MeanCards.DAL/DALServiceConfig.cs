using MeanCards.Configuration;
using MeanCards.DAL.Initializer;
using MeanCards.DAL.Interfaces.Initializer;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Repository;
using MeanCards.DAL.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeanCards.DAL
{
    public static class DALServiceConfig
    {
        public static IServiceCollection RegisterContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>((opt) =>
            opt.UseSqlServer(
                GetConnectionString(configuration),
                cb => cb.MigrationsHistoryTable(AppDbContext.DefaultMigrationsTable)),
            ServiceLifetime.Scoped);

            return services;
        }

        public static IServiceCollection RegisterDAL(this IServiceCollection services)
        {
            //Repositories
            services
                .AddScoped<ILanguagesRepository, LanguagesRepository>()
                .AddScoped<IQuestionCardsRepository, QuestionCardsRepository>()
                .AddScoped<IAnswerCardsRepository, AnswerCardsRepository>()
                .AddScoped<IUsersRepository, UsersRepository>()
                .AddScoped<IGamesRepository, GamesRepository>()
                .AddScoped<IGameRoundsRepository, GameRoundsRepository>()
                .AddScoped<IPlayerAnswersRepository, PlayerAnswersRepository>()
                .AddScoped<IPlayersRepository, PlayersRepository>()
                .AddScoped<IPlayerCardsRepository, PlayerCardsRepository>();

            //Initializers
            services
                .AddTransient<ILanguageInitializer, LanguageInitializer>()
                .AddTransient<IDbInitializer, DbInitializer>();

            return services;
        }

        private static string GetConnectionString(IConfiguration configuration)
        {
            return configuration[ConfigurationProperties.DbSettings.ConnectionString].ToString();
        }
    }
}

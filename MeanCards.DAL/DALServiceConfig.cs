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
                .AddScoped<ILanguagesRepository, LanguagesRepository>();
            services
                .AddScoped<IQuestionCardsRepository, QuestionCardsRepository>();
            services
                .AddScoped<IAnswerCardsRepository, AnswerCardsRepository>();
            services
                .AddScoped<IUsersRepository, UsersRepository>();
            services
                .AddScoped<IGamesRepository, GamesRepository>();

            //Initializers
            services
                .AddTransient<ILanguageInitializer, LanguageInitializer>();

            return services;
        }

        private static string GetConnectionString(IConfiguration configuration)
        {
            return configuration[ConfigurationProperties.DbSettings.ConnectionString].ToString();
        }
    }
}

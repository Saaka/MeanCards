using MeanCards.Common.Constants;
using MeanCards.Configuration;
using MeanCards.DAL.Initializer;
using MeanCards.DAL.Interfaces.Initializer;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Repository;
using MeanCards.DAL.Storage;
using MeanCards.DAL.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MeanCards.DAL.Transaction;
using MeanCards.DAL.Interfaces.Transactions;

namespace MeanCards.DAL
{
    public static class DALServiceConfig
    {
        public static IServiceCollection RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>((opt) =>
            opt.UseSqlServer(
                GetConnectionString(configuration),
                cb =>
                {
                    cb.MigrationsHistoryTable(AppDbContext.DefaultMigrationsTable);
                }),
            ServiceLifetime.Scoped);

            services
                .AddTransient<IRepositoryTransactionsFactory, DbContextRepositoryTransactionsFactory>();

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
                .AddScoped<IPlayerCardsRepository, PlayerCardsRepository>()
                .AddScoped<IGameCheckpointRepository, GameCheckpointRepository>();
            
            //Initializers
            services
                .AddTransient<ILanguageInitializer, LanguageInitializer>()
                .AddTransient<IDbInitializer, DbInitializer>();

            return services;
        }

        public static IServiceCollection RegisterIdentityStore(this IServiceCollection services)
        {
            services
                .AddIdentity<User, IdentityUserRole<int>>(opt =>
                {
                    opt.User.RequireUniqueEmail = true;
                    opt.User.AllowedUserNameCharacters = AuthConstants.AllowedUserNameCharacters;
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequiredLength = AuthConstants.MinPasswordLength;
                })
                .AddUserStore<UserStore<User, IdentityRole<int>, AppDbContext, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityUserToken<int>, IdentityRoleClaim<int>>>()
                ;

            return services;
        }

        private static string GetConnectionString(IConfiguration configuration)
        {
            return configuration[ConfigurationProperties.DbSettings.ConnectionString].ToString();
        }
    }
}

using MeanCards.GameManagement;
using MeanCards.GameManagement.CoreServices;
using MeanCards.Model.Core.Games;
using MeanCards.Model.Core.Users;
using MeanCards.UserManagement;
using MeanCards.Validators;
using MeanCards.Validators.Games;
using MeanCards.Validators.Users;
using Microsoft.Extensions.DependencyInjection;

namespace MeanCards
{
    public static class MeanCardsServiceConfig
    {
        public static IServiceCollection RegisterDomainServices(this IServiceCollection services)
        {
            services

                //Handlers
                .AddScoped<ICreateUserHandler, CreateUserHandler>()
                .AddScoped<IAuthenticateGoogleUserHandler, AuthenticateGoogleUserHandler>()

                .AddScoped<ICreateGameHandler, CreateGameHandler>()
                .AddScoped<IJoinGameHandler, JoinGameHandler>()
                .AddScoped<IStartGameRoundHandler, StartGameRoundHandler>()
                .AddScoped<ISubmitAnswerHandler, SubmitAnswerHandler>()

                //query handlers
                .AddScoped<IGetUserByCredentialsHandler, GetUserByCredentialsHandler>()
                .AddScoped<IGetUserByCodeHandler, GetUserByCodeHandler>()

                //Core services
                .AddScoped<IGameCheckpointUpdater, GameCheckpointUpdater>()

                //validators
                .AddTransient<IRequestValidator<CreateUser>, CreateUserValidator>()
                .AddTransient<IRequestValidator<AuthenticateGoogleUser>, AuthenticateGoogleUserValidator>()
                .AddTransient<IRequestValidator<CreateGame>, CreateGameValidator>()
                .AddTransient<IRequestValidator<JoinGame>, JoinGameValidator>()
                .AddTransient<IRequestValidator<StartGameRound>, StartGameRoundValidator>()
                .AddTransient< IRequestValidator<SubmitAnswer>, SubmitAnswerValidator>();

            return services;
        }
    }
}

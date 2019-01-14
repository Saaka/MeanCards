using MeanCards.GameManagement;
using MeanCards.GameManagement.CoreServices;
using MeanCards.Model.Core;
using MeanCards.Model.Core.Games;
using MeanCards.Model.Core.Games.Base;
using MeanCards.Model.Core.Users;
using MeanCards.UserManagement;
using MeanCards.Validators;
using MeanCards.Validators.Games;
using MeanCards.Validators.Games.Base;
using MeanCards.Validators.Games.ValidationRules;
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
                .AddScoped<IEndSubmissionsHandler, EndSubmissionsHandler>()
                .AddScoped<ISkipRoundHandler, SkipRoundHandler>()
                .AddScoped<IEndGameHandler, EndGameHandler>()

                //query handlers
                .AddScoped<IGetUserByCredentialsHandler, GetUserByCredentialsHandler>()
                .AddScoped<IGetUserByCodeHandler, GetUserByCodeHandler>()

                //Core services
                .AddScoped<IGameCheckpointUpdater, GameCheckpointUpdater>()
                .AddScoped<INextGameRoundOwnerProvider, NextGameRoundOwnerProvider>()
                .AddScoped<IGameRoundCreator, GameRoundCreator>()
                .AddScoped<IPlayerCardsCreator, PlayerCardsCreator>()
                
                //validators

                .AddTransient<IBaseGameRequestsValidator, BaseGameRequestsValidator>()
                .AddTransient<IRequestValidator<IGameRequest>, GameRequestValidator>()
                .AddTransient<IRequestValidator<IGameRoundRequest>, GameRoundRequestValidator>()
                .AddTransient<IRequestValidator<IUserRequest>, UserRequestValidator>()
                .AddTransient<IRequestValidator<IPlayerRequest>, PlayerRequestValidator>()
                .AddTransient<IRequestValidator<CreateUser>, CreateUserValidator>()
                .AddTransient<IRequestValidator<AuthenticateGoogleUser>, AuthenticateGoogleUserValidator>()
                .AddTransient<IRequestValidator<CreateGame>, CreateGameValidator>()
                .AddTransient<IRequestValidator<JoinGame>, JoinGameValidator>()
                .AddTransient<IRequestValidator<StartGameRound>, StartGameRoundValidator>()
                .AddTransient<IRequestValidator<SubmitAnswer>, SubmitAnswerValidator>()
                .AddTransient<IRequestValidator<EndSubmissions>, EndSubmissionsValidator>()
                .AddTransient<IRequestValidator<SkipRound>, SkipRoundValidator>()
                .AddTransient<IRequestValidator<EndGame>, EndGameValidator>()

                .AddTransient<IGameOrRoundOwnerRule, GameOrRoundOwnerRule>()
                ;

            return services;
        }
    }
}

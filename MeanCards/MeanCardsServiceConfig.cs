using MeanCards.GameManagement;
using MeanCards.Model.Core.Users;
using MeanCards.UserManagement;
using MeanCards.Validators;
using MeanCards.Validators.User;
using Microsoft.Extensions.DependencyInjection;

namespace MeanCards
{
    public static class MeanCardsServiceConfig
    {
        public static IServiceCollection RegisterDomainServices(this IServiceCollection services)
        {
            services
                
                //Handlers
                .AddScoped<ICreateGameHandler, CreateGameHandler>()
                .AddScoped<ICreateUserHandler, CreateUserHandler>()
                .AddScoped<IAuthenticateGoogleUserHandler, AuthenticateGoogleUserHandler>()

                //query handlers
                .AddScoped<IGetUserByCredentialsHandler, GetUserByCredentialsHandler>()

                //validators
                .AddTransient<IRequestValidator<CreateUser>, CreateUserValidator>()
                .AddTransient<IRequestValidator<AuthenticateGoogleUser>, AuthenticateGoogleUserValidator>();

            return services;
        }
    }
}

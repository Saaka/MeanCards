using MeanCards.Commands.Users;
using MeanCards.GameManagement;
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
            //Handlers
            services
                .AddScoped<ICreateGameHandler, CreateGameHandler>()
                .AddScoped<ICreateUserHandler, CreateUserHandler>()
                
                .AddTransient<ICommandValidator<CreateUser>, CreateUserValidator>();

            return services;
        }
    }
}

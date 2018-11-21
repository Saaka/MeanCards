using MeanCards.GameManagement;
using MeanCards.UserManagement;
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
                .AddScoped<ICreateUserHandler, CreateUserHandler>();

            return services;
        }
    }
}

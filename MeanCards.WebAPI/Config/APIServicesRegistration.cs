using MeanCards.WebAPI.Controllers.Base;
using MeanCards.WebAPI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MeanCards.WebAPI.Config
{
    public static class APIServicesRegistration
    {
        public static IServiceCollection RegisterAPIServices(this IServiceCollection services)
        {
            services
                .AddScoped<IGameDataProvider, GameDataProvider>()
                .AddScoped<IGameDataService, GameDataService>()
                .AddScoped<IGameRoundDataService, GameRoundDataService>();

            return services;
        }
    }
}

using MeanCards.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MeanCards.Configuration;
using MeanCards.DAL;

namespace MeanCards.WebAPI.Config
{
    public static class AppServiceConfiguration
    {
        public static IServiceCollection RegisterModules(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .RegisterLibs()
                .RegisterDomainServices()
                .RegisterCommon()
                .RegisterConfiguration()
                .RegisterDbContext(configuration)
                .RegisterDAL()
                .RegisterAPIServices()
                .RegisterIdentityStore();

            return services;
        }

        public static IServiceCollection RegisterLibs(this IServiceCollection services)
        {
            return services
                .AddMemoryCache();
        }
    }
}

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
                .RegisterDomainServices()
                .RegisterCommon()
                .RegisterConfiguration()
                .RegisterContext(configuration)
                .RegisterDAL()
                .RegisterIdentityStore()
                ;

            return services;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;

namespace MeanCards.Configuration
{
    public static class ConfigurationRegistrationModule
    {
        public static IServiceCollection RegisterConfiguration(this IServiceCollection services)
        {
            services
                .AddScoped<IDbConnectionConfig, ApplicationConfiguration>()
                .AddScoped<IAuthConfiguration, ApplicationConfiguration>();

            return services;
        }
    }
}

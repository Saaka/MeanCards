using MeanCards.Common.Helpers;
using MeanCards.Common.RandomCodeProvider;
using Microsoft.Extensions.DependencyInjection;

namespace MeanCards.Common
{
    public static class CommonServiceConfig
    {
        public static IServiceCollection RegisterCommon(this IServiceCollection services)
        {
            services
                .AddTransient<GuidEncoder>()
                .AddTransient<HashGenerator>()
                .AddTransient<ICodeGenerator, GuidRandomCodeGenerator>();
                
            return services;
        }
    }
}

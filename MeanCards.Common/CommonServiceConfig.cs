using MeanCards.Common.Helpers;
using MeanCards.Common.ProfileImageUrlProvider;
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
                .AddTransient<ICodeGenerator, GuidRandomCodeGenerator>()
                .AddTransient<IProfileImageUrlProvider, GravatarImageUrlProvider>();

            return services;
        }
    }
}

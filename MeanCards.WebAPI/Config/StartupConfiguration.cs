using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace MeanCards.WebAPI.Config
{
    public static class StartupConfiguration
    {
        public static IWebHostBuilder ConfigureStartup(this IWebHostBuilder builder)
        {
            builder
                .ConfigureLogging(log =>
                {
                    log.AddConsole();
                });

            return builder;
        }
    }
}

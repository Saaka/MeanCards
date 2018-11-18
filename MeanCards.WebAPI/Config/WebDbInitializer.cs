using MeanCards.DAL.Interfaces.Initializer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace MeanCards.WebAPI.Config
{
    public class WebDbInitializer
    {
        public static void Initialize(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var initializer = services.GetRequiredService<IDbInitializer>();
                    initializer.Execute().Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<WebDbInitializer>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
        }
    }
}

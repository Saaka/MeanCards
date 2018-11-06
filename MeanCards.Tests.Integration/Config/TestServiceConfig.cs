using MeanCards.DAL.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MeanCards.Tests.Integration.Config
{
    public static class TestServiceConfig
    {
        public static IServiceCollection RegisterInmemoryContext(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("test_db"));

            return services;
        }
    }
}

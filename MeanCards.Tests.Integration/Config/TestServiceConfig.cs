using MeanCards.DAL.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MeanCards.Tests.Integration.Config
{
    public static class TestServiceConfig
    {
        public static IServiceCollection RegisterInmemoryContext(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            return services;
        }
    }
}

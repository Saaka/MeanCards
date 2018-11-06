using MeanCards.DAL.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeanCards.Tests.Integration.Config
{
    public static class TestServiceConfig
    {
        public static IServiceCollection RegisterContext(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("test_db_meancards"));

            return services;
        }
    }
}

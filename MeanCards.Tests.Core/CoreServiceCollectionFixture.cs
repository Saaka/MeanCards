using MeanCards.Common;
using MeanCards.DAL;
using MeanCards.DAL.Interfaces.Initializer;
using MeanCards.DAL.Storage;
using MeanCards.Tests.Base.Fixtures;
using MeanCards.Tests.Core.Config;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MeanCards.Tests.Core
{
    public class CoreServiceCollectionFixture : ServiceCollectionFixture
    {
        public override IServiceCollection RegisterServices(IServiceCollection serviceCollection)
        {
            var config = CoreTestsConfiguration.InitConfiguration();
            var databaseName = $"MC_TESTS_{Guid.NewGuid().ToString("N")}";
            return serviceCollection
                .RegisterCoreTestsContext(config, databaseName)
                .RegisterDAL()
                .RegisterIdentityStore()
                .RegisterDomainServices()
                .RegisterCommon();
        }

        protected override void OnServiceCollectionInitialized()
        {
            var initializer = GetService<IDbInitializer>();

            initializer.Execute().Wait();
        }

        private bool isDisposed = false;
        public override void Dispose()
        {
            if(!isDisposed)
            {
                isDisposed = true;
                var context = GetService<AppDbContext>();
                context.Database.EnsureDeleted();
            }
        }
    }
}

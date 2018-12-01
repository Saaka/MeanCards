using Microsoft.Extensions.DependencyInjection;
using System;

namespace MeanCards.Tests.Base.Fixtures
{
    public abstract class ServiceCollectionFixture : IDisposable
    {
        public readonly IServiceProvider ServiceProvider;
        public ServiceCollectionFixture()
        {
            var serviceCollection = new ServiceCollection();
            RegisterServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            OnServiceCollectionInitialized();
        }

        protected virtual void OnServiceCollectionInitialized()
        {

        }

        public abstract IServiceCollection RegisterServices(IServiceCollection serviceCollection);

        public virtual void Dispose()
        {
        }

        public T GetService<T>()
        {
            return ServiceProvider.GetService<T>();
        }
    }
}

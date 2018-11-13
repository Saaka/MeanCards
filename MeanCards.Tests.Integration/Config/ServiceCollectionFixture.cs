using Microsoft.Extensions.DependencyInjection;
using System;

namespace MeanCards.Tests.Integration.Config
{
    public abstract class ServiceCollectionFixture : IDisposable
    {
        public readonly IServiceProvider ServiceProvider;
        public ServiceCollectionFixture()
        {
            var serviceCollection = new ServiceCollection();
            RegisterServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
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

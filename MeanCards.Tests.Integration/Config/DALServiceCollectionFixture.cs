using MeanCards.DAL;
using Microsoft.Extensions.DependencyInjection;

namespace MeanCards.Tests.Integration.Config
{
    public class DALServiceCollectionFixture : ServiceCollectionFixture
    {
        public override IServiceCollection RegisterServices(IServiceCollection serviceCollection)
        {
            return serviceCollection
                .RegisterInmemoryContext()
                .RegisterDAL();
        }
    }
}

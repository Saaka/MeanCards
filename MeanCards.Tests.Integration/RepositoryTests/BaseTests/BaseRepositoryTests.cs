using MeanCards.Tests.Integration.Config;
using System;

namespace MeanCards.Tests.Integration.RepositoryTests
{
    public abstract class BaseRepositoryTests : IDisposable
    {
        protected readonly DALServiceCollectionFixture Fixture;

        public BaseRepositoryTests()
        {
            Fixture = new DALServiceCollectionFixture();
        }

        public void Dispose()
        {
            Fixture.Dispose();
        }
    }
}

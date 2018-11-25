using MeanCards.Tests.Integration.Config;
using System;

namespace MeanCards.Tests.Integration.BaseTests
{
    public class BaseDomainTests : IDisposable
    {
        protected readonly DomainServiceCollectionFixture Fixture;

        public BaseDomainTests()
        {
            Fixture = new DomainServiceCollectionFixture();
        }

        public void Dispose()
        {
            Fixture.Dispose();
        }
    }
}

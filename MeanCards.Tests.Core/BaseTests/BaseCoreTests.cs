using System;

namespace MeanCards.Tests.Core
{
    public class BaseCoreTests : IDisposable
    {
        protected readonly CoreServiceCollectionFixture Fixture;

        public BaseCoreTests()
        {
            Fixture = new CoreServiceCollectionFixture();
        }

        public void Dispose()
        {
            Fixture.Dispose();
        }
    }
}

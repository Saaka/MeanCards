using MeanCards.DAL.Interfaces.Transactions;

namespace MeanCards.Tests.Integration.BaseTests.Transactions
{
    public class SqliteMockTransactionScope : IRepositoryTransaction
    {
        public void CommitTransaction()
        {
        }

        public void Dispose()
        {
        }
    }
}

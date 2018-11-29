using MeanCards.DAL.Interfaces.Transactions;

namespace MeanCards.Tests.Integration.BaseTests.Transactions
{
    public class SqliteMockTransactionScopeFactory : IRepositoryTransactionsFactory
    {
        public IRepositoryTransaction CreateTransaction()
        {
            return new SqliteMockTransactionScope();
        }
    }
}

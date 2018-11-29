using MeanCards.DAL.Interfaces.Transactions;

namespace MeanCards.DAL.Transaction
{
    public class RepositoryTransactionsFactory : IRepositoryTransactionsFactory
    {
        public IRepositoryTransaction CreateTransaction()
        {
            return new RepositoryTransaction();
        }
    }
}

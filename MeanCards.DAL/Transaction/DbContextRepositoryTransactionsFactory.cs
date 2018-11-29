using MeanCards.DAL.Interfaces.Transactions;

namespace MeanCards.DAL.Transaction
{
    public class DbContextRepositoryTransactionsFactory : IRepositoryTransactionsFactory
    {
        public IRepositoryTransaction CreateTransaction()
        {
            return new DbContextRepositoryTransaction();
        }
    }
}

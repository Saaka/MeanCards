using MeanCards.DAL.Interfaces.Transactions;
using System.Transactions;

namespace MeanCards.DAL.Transaction
{
    public class DbContextRepositoryTransaction : IRepositoryTransaction
    {
        protected readonly TransactionScope scope;

        public DbContextRepositoryTransaction()
        {
            scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled);
        }

        public void CommitTransaction()
        {
            scope.Complete();
        }

        public void Dispose()
        {
            if(scope != null)
            {
                scope.Dispose();
            }
        }
    }
}

using MeanCards.DAL.Interfaces.Transactions;
using System.Transactions;

namespace MeanCards.DAL.Transaction
{
    public class RepositoryTransaction : IRepositoryTransaction
    {
        protected readonly TransactionScope scope;

        public RepositoryTransaction()
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

using System;

namespace MeanCards.DAL.Interfaces.Transactions
{
    public interface IRepositoryTransaction : IDisposable
    {
        void CommitTransaction();
    }
}

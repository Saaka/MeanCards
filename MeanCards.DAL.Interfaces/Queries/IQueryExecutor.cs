using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Queries
{
    public interface IQueryExecutor
    {
        Task<T> QueryFirstOrDefault<T>(string sql, object parameters = null);
        Task<IEnumerable<T>> Query<T>(string sql, object parameters = null);
        Task<int> Execute(string sql, object parameters = null);
    }
}

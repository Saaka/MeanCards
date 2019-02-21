using Dapper;
using MeanCards.DAL.Connections;
using MeanCards.DAL.Interfaces.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeanCards.DAL.Queries
{
    public class SqlQueryExecutor : IQueryExecutor
    {
        protected readonly IDbConnectionFactory dbConnectionFactory;

        public SqlQueryExecutor(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public virtual async Task<T> QueryFirstOrDefault<T>(string sql, object parameters = null)
        {
            using (var connection = dbConnectionFactory.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
            }
        }

        public virtual async Task<IEnumerable<T>> Query<T>(string sql, object parameters = null)
        {
            using (var connection = dbConnectionFactory.CreateConnection())
            {
                return await connection.QueryAsync<T>(sql, parameters);
            }
        }

        public virtual async Task<int> Execute(string sql, object parameters = null)
        {
            using (var connection = dbConnectionFactory.CreateConnection())
            {
                return await connection.ExecuteAsync(sql, parameters);
            }
        }
    }
}

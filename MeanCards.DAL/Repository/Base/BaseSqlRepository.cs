using Dapper;
using MeanCards.DAL.Connections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeanCards.DAL.Repository.Base
{
    public class BaseSqlRepository
    {
        protected readonly IDbConnectionFactory dbConnectionFactory;

        public BaseSqlRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        protected virtual async Task<T> QueryFirstOrDefault<T>(string sql, object parameters = null)
        {
            using (var connection = dbConnectionFactory.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
            }
        }

        protected virtual async Task<IEnumerable<T>> Query<T>(string sql, object parameters = null)
        {
            using (var connection = dbConnectionFactory.CreateConnection())
            {
                return await connection.QueryAsync<T>(sql, parameters);
            }
        }

        protected virtual async Task<int> Execute(string sql, object parameters = null)
        {
            using (var connection = dbConnectionFactory.CreateConnection())
            {
                return await connection.ExecuteAsync(sql, parameters);
            }
        }
    }
}

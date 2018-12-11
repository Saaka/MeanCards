using Dapper;
using MeanCards.DAL.Connections;
using System.Collections.Generic;
using System.Linq;

namespace MeanCards.DAL.Repository.Base
{
    public class BaseSqlRepository
    {
        protected readonly IDbConnectionFactory dbConnectionFactory;

        public BaseSqlRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        protected virtual T QueryFirstOrDefault<T>(string sql, object parameters = null)
        {
            using (var connection = dbConnectionFactory.CreateConnection())
            {
                return connection.QueryFirstOrDefault<T>(sql, parameters);
            }
        }

        protected virtual List<T> Query<T>(string sql, object parameters = null)
        {
            using (var connection = dbConnectionFactory.CreateConnection())
            {
                return connection.Query<T>(sql, parameters).ToList();
            }
        }

        protected virtual int Execute(string sql, object parameters = null)
        {
            using (var connection = dbConnectionFactory.CreateConnection())
            {
                return connection.Execute(sql, parameters);
            }
        }
    }
}

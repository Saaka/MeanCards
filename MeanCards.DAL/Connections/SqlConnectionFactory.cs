using MeanCards.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MeanCards.DAL.Connections
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
        IDbConnection CreateConnection(string connectionString);
    }

    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly IDbConnectionConfig dbConnectionConfig;

        public SqlConnectionFactory(IDbConnectionConfig dbConnectionConfig)
        {
            this.dbConnectionConfig = dbConnectionConfig;
        }

        public IDbConnection CreateConnection()
        {
            return CreateConnection(dbConnectionConfig.GetConnectionString());
        }

        public IDbConnection CreateConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}

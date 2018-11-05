using Microsoft.Extensions.Configuration;

namespace MeanCards.Configuration
{
    public class ApplicationConfiguration : IDbConnectionConfig
    {
        private readonly IConfiguration _config;

        public ApplicationConfiguration(IConfiguration config)
        {
            _config = config;
        }

        private string DbConnectionString => _config[ConfigurationProperties.DbSettings.ConnectionString].ToString();

        public string GetConnectionString()
        {
            return DbConnectionString;
        }
    }
}

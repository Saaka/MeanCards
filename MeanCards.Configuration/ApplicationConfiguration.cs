using Microsoft.Extensions.Configuration;
using System;

namespace MeanCards.Configuration
{
    public class ApplicationConfiguration : IDbConnectionConfig, IAuthConfiguration
    {
        private const int DefaultExpirationTime = 10080;
        private readonly IConfiguration _config;

        public ApplicationConfiguration(IConfiguration config)
        {
            _config = config;
        }

        private string DbConnectionString;
        public string GetConnectionString()
        {
            if(DbConnectionString == null)
                DbConnectionString = _config[ConfigurationProperties.DbSettings.ConnectionString];

            return DbConnectionString;
        }

        private string AuthSecret;
        public string GetSecret()
        {
            if (AuthSecret == null)
                AuthSecret = _config[ConfigurationProperties.Auth.Secret];

            return AuthSecret;
        }

        private int ExpirationTime = 0;
        public int GetExpirationTimeInMinutes()
        {
            if (ExpirationTime == 0)
                ExpirationTime = _config[ConfigurationProperties.Auth.ExpirationInMinutes] != null 
                    ? Convert.ToInt32(_config[ConfigurationProperties.Auth.ExpirationInMinutes]) : DefaultExpirationTime;

            return ExpirationTime;
        }

        private string Issuer;
        public string GetIssuer()
        {
            if (Issuer == null)
                Issuer = _config[ConfigurationProperties.Auth.Issuer];

            return Issuer;
        }
    }
}

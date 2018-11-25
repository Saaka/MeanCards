using Microsoft.Extensions.Configuration;
using System;

namespace MeanCards.Configuration
{
    public class ApplicationConfiguration : IDbConnectionConfig, IAuthConfiguration, IGoogleConfig
    {
        private const int DefaultExpirationTime = 10080;
        private readonly IConfiguration _config;

        public ApplicationConfiguration(IConfiguration config)
        {
            _config = config;
        }

        private string dbConnectionString;
        public string GetConnectionString()
        {
            if(dbConnectionString == null)
                dbConnectionString = _config[ConfigurationProperties.DbSettings.ConnectionString];

            return dbConnectionString;
        }

        private string authSecret;
        public string GetSecret()
        {
            if (authSecret == null)
                authSecret = _config[ConfigurationProperties.Auth.Secret];

            return authSecret;
        }

        private int expirationTime = 0;
        public int GetExpirationTimeInMinutes()
        {
            if (expirationTime == 0)
                expirationTime = _config[ConfigurationProperties.Auth.ExpirationInMinutes] != null 
                    ? Convert.ToInt32(_config[ConfigurationProperties.Auth.ExpirationInMinutes]) : DefaultExpirationTime;

            return expirationTime;
        }

        private string issuer;
        public string GetIssuer()
        {
            if (issuer == null)
                issuer = _config[ConfigurationProperties.Auth.Issuer];

            return issuer;
        }

        private string googleClientId;
        public string GetClientId()
        {
            if (googleClientId == null)
                googleClientId = _config[ConfigurationProperties.Google.ClientId];

            return googleClientId;
        }
    }
}

namespace MeanCards.Configuration
{
    public class ConfigurationProperties
    {
        public class DbSettings
        {
            public const string ConnectionString = "DbSettings:ConnectionString";
        }
        public class Google
        {
            public const string ClientId = "Google:ClientId";
            public const string ClientKey = "Google:ClientKey";
            public const string ValidationEndpoint = "Google:ValidationEndpoint";
        }
        public class Auth
        {
            public const string Secret = "Auth:Secret";
            public const string ExpirationInMinutes = "Auth:ExpirationInMinutes";
            public const string Issuer = "Auth:Issuer";
        }
    }
}

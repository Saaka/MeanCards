namespace MeanCards.Configuration
{
    public interface IAuthConfiguration
    {
        string GetSecret();
        int GetExpirationTimeInMinutes();
        string GetIssuer();
    }
}

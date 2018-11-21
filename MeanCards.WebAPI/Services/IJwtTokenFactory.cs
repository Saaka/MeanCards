namespace MeanCards.WebAPI.Services
{
    public interface IJwtTokenFactory
    {
        string GenerateEncodedToken(string userName);
    }
}
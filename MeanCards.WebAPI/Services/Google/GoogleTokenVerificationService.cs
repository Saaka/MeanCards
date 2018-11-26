using MeanCards.Configuration;
using System.Threading.Tasks;

namespace MeanCards.WebAPI.Services.Google
{
    public interface IGoogleTokenVerificationService
    {
        Task<ValidateGoogleTokenResult> Validate(string googleToken);
    }

    public class GoogleTokenVerificationService : IGoogleTokenVerificationService
    {
        private readonly GoogleClient googleClient;
        private readonly IGoogleConfig googleConfig;

        public GoogleTokenVerificationService(
            GoogleClient googleClient, 
            IGoogleConfig googleConfig)
        {
            this.googleClient = googleClient;
            this.googleConfig = googleConfig;
        }

        public async Task<ValidateGoogleTokenResult> Validate(string googleToken)
        {
            var tokenInfo = await googleClient.GetTokenInfo(googleToken);
            if (tokenInfo == null || 
                tokenInfo.ClientId != googleConfig.GetClientId())
                return new ValidateGoogleTokenResult { IsSuccessful = false, Error =  "InvalidToken"};

            return new ValidateGoogleTokenResult
            {
                IsSuccessful = true,
                TokenInfo = tokenInfo
            };
        }
    }
}

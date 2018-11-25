namespace MeanCards.WebAPI.Services.Google
{
    public class ValidateGoogleTokenResult
    {
        public bool IsSuccessful { get; set; }
        public string Error { get; set; }
        public GoogleTokenInfo TokenInfo { get; set; }
    }
}

namespace MeanCards.ViewModel.Auth
{
    public class AuthenticateUserResult
    {
        public AuthenticateUserResult(string error)
        {
            IsSuccessful = false;
            Error = error;
        }

        public AuthenticateUserResult()
        {
            IsSuccessful = true;
        }

        public bool IsSuccessful { get; set; }
        public string Token { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Error { get; set; }
    }
}

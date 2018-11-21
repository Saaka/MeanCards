namespace MeanCards.Commands.Users
{
    public class CreateUserResult
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string UserCode { get; set; }
        public string DisplayName { get; set; }
        public string ImageUrl { get; set; }
    }
}

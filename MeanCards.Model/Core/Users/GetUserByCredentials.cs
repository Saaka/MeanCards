using MediatR;

namespace MeanCards.Model.Core.Users
{
    public class GetUserByCredentials : IRequest<GetUserByCredentialsResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

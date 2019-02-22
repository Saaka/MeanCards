using MediatR;

namespace MeanCards.Model.Core.Users
{
    public class CreateUser : IRequest<CreateUserResult>
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

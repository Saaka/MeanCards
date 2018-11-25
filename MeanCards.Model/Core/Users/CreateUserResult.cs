using MeanCards.Model.DTO.Users;

namespace MeanCards.Model.Core.Users
{
    public class CreateUserResult : BaseResult
    {
        public CreateUserResult()
        {
        }

        public CreateUserResult(string error) : base(error)
        {
        }

        public UserModel User { get; set; }
    }
}

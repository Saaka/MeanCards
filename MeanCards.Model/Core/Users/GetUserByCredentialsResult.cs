using MeanCards.Model.DTO.Users;

namespace MeanCards.Model.Core.Users
{
    public class GetUserByCredentialsResult : BaseResult
    {
        public GetUserByCredentialsResult()
        {
        }

        public GetUserByCredentialsResult(string error) : base(error)
        {
        }

        public UserModel User { get; set; }
    }
}

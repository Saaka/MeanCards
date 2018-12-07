using MeanCards.Model.DTO.Users;

namespace MeanCards.Model.Core.Users
{
    public class GetUserByCodeResult : BaseResult
    {
        public GetUserByCodeResult()
        {
        }

        public GetUserByCodeResult(string error) : base(error)
        {
        }

        public UserModel User { get; set; }
    }
}

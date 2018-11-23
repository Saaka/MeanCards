using MeanCards.Model.Access.Users;
using MeanCards.Model.Creation.Users;
using MeanCards.Model.DTO.Users;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IUsersRepository
    {
        Task<int> CreateUser(CreateUserModel model);
        Task<bool> UserEmailExists(string email);
        Task<bool> UserNameExists(string displayName);
        Task<UserModel> GetUserByCredentials(GetUserByCredentialsModel model);
    }
}

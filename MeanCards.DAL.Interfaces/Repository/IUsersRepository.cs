using MeanCards.Model.DAL.Access.Users;
using MeanCards.Model.DAL.Creation.Users;
using MeanCards.Model.DAL.Modification.Users;
using MeanCards.Model.DTO.Users;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IUsersRepository
    {
        Task<UserModel> CreateUser(CreateUserModel model);
        Task<UserModel> CreateGoogleUser(CreateGoogleUserModel model);
        Task<UserModel> UpdateGoogleUser(UpdateGoogleUserModel model);
        Task<UserModel> MergeUserWithGoogle(MergeUserWithGoogleModel model);
        Task<bool> UserEmailExists(string email);
        Task<bool> UserNameExists(string displayName);
        Task<bool> GoogleUserExists(string email, string googleId);
        Task<UserModel> GetUserByCredentials(GetUserByCredentialsModel model);
    }
}

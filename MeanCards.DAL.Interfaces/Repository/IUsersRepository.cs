using MeanCards.Model.DAL;
using MeanCards.Model.DAL.Access.Users;
using MeanCards.Model.DAL.Creation.Users;
using MeanCards.Model.DAL.Modification.Users;
using MeanCards.Model.DTO.Users;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IUsersRepository
    {
        Task<RepositoryResult<UserModel>> CreateUser(CreateUserModel model);
        Task<RepositoryResult<UserModel>> CreateGoogleUser(CreateGoogleUserModel model);
        Task<RepositoryResult<UserModel>> UpdateGoogleUser(UpdateGoogleUserModel model);
        Task<RepositoryResult<UserModel>> MergeUserWithGoogle(MergeUserWithGoogleModel model);
        Task<bool> UserEmailExists(string email);
        Task<bool> UserNameExists(string displayName);
        Task<bool> GoogleUserExists(string email, string googleId);
        Task<RepositoryResult<UserModel>> GetUserByCredentials(GetUserByCredentialsModel model);
    }
}

using MeanCards.Model.Creation.Users;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IUsersRepository
    {
        Task<int> CreateUser(CreateUserModel model);
        Task<bool> UserEmailExists(string email);
        Task<bool> UserNameExists(string displayName);
    }
}

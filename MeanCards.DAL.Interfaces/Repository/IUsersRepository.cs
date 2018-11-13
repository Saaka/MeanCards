using MeanCards.DataModel.Entity;
using MeanCards.Model.Creation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IUsersRepository
    {
        Task<int> CreateUser(CreateUserModel model);
        Task<List<User>> GetAllActiveUsers();
    }
}

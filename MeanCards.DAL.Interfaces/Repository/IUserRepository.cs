using MeanCards.Model.ViewModel;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<int> CreateUser(CreateUserModel model);
    }
}

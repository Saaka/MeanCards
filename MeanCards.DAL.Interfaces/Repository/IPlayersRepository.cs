using MeanCards.DataModel.Entity;
using MeanCards.Model.Creation;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IPlayersRepository
    {
        Task<int> CreatePlayer(CreatePlayerModel model);
        Task<Player> GetPlayerById(int playerId);
    }
}

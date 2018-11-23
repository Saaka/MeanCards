using MeanCards.Model.Creation;
using MeanCards.Model.DTO.Players;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IPlayersRepository
    {
        Task<int> CreatePlayer(CreatePlayerModel model);
        Task<PlayerModel> GetPlayerById(int playerId);
    }
}

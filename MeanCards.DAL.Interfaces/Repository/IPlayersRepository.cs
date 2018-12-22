using MeanCards.Model.DAL.Creation.Players;
using MeanCards.Model.DTO.Players;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IPlayersRepository
    {
        Task<PlayerModel> CreatePlayer(CreatePlayerModel model);
        Task<PlayerModel> GetPlayerById(int playerId);
        Task<PlayerModel> GetPlayerByUserId(int userId, int gameId);
        Task<int> GetMaxPlayerNumberForGame(int gameId);
    }
}

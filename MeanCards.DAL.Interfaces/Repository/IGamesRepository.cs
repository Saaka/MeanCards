using MeanCards.Common.Enums;
using MeanCards.Model.DAL.Creation.Games;
using MeanCards.Model.DTO.Games;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IGamesRepository
    {
        Task<GameModel> CreateGame(CreateGameModel model);
        Task<GameModel> GetGameById(int gameId);
        Task<GameStatusEnum> GetGameStatus(int gameId);
        Task<GameSimpleModel> GetGameByCode(string code);
        Task<bool> CancelGame(int gameId);
        Task<bool> EndGame(int gameId, int userId);
        Task<bool> GameNameIsTaken(string name, int userId);
    }
}

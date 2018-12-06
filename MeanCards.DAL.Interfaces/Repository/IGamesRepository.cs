using MeanCards.Model.DAL.Creation.Games;
using MeanCards.Model.DTO.Games;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IGamesRepository
    {
        Task<bool> ActiveGameExists(int gameId);
        Task<bool> IsUserInGame(int gameId, int userId);
        Task<GameModel> CreateGame(CreateGameModel model);
        Task<GameModel> GetGameById(int gameId);
        Task<GameModel> GetGameByCode(string code);
    }
}

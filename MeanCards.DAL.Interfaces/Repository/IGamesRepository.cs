using MeanCards.Model.Creation;
using MeanCards.Model.DTO.Games;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IGamesRepository
    {
        Task<int> CreateGame(CreateGameModel model);
        Task<GameModel> GetGameById(int gameId);
        Task<GameModel> GetGameByCode(string code);
    }
}

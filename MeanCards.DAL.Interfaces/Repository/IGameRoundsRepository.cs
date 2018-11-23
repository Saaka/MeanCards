using MeanCards.Model.Creation;
using MeanCards.Model.DTO.Games;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IGameRoundsRepository
    {
        Task<int> CreateGameRound(CreateGameRoundModel model);
        Task<GameRoundModel> GetCurrentGameRound(int gameId);
    }
}

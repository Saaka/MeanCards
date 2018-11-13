using MeanCards.DataModel.Entity;
using MeanCards.Model.Creation;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IGameRoundsRepository
    {
        Task<int> CreateGameRound(CreateGameRoundModel model);
        Task<GameRound> GetCurrentGameRound(int gameId);
    }
}

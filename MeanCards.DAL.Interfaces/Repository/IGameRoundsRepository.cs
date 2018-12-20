using MeanCards.Model.DAL.Creation.Games;
using MeanCards.Model.DTO.Games;
using MeanCards.Model.DTO.Players;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IGameRoundsRepository
    {
        Task<GameRoundModel> CreateGameRound(CreateGameRoundModel model);
        Task<GameRoundModel> GetCurrentGameRound(int gameId);
        Task<bool> IsGameRoundPending(int gameRoundId);
        Task<bool> IsGameRoundInProgress(int gameRoundId);
        Task<bool> IsGameRoundOwner(int gameRoundId, int userId);
        Task<bool> IsRoundInGame(int gameId, int gameRoundId);
        Task<bool> StartGameRound(int gameRoundId);
    }
}

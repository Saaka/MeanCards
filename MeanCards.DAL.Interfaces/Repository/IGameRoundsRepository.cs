using MeanCards.Common.Enums;
using MeanCards.Model.DAL.Creation.Games;
using MeanCards.Model.DTO.Games;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IGameRoundsRepository
    {
        Task<GameRoundModel> CreateGameRound(CreateGameRoundModel model);
        Task<GameRoundModel> GetCurrentGameRound(int gameId);
        Task<GameRoundModel> GetGameRound(int gameId, int gameRoundId);
        Task<bool> UpdateGameRoundStatus(int gameRoundId, GameRoundStatusEnum status);
        Task<bool> SkipRound(int gameRoundId);
        Task<bool> SelectRoundWinner(int gameRoundId, int playerId);
        Task<GameRoundSimpleModel> GetGameRoundByCode(string code);
    }
}

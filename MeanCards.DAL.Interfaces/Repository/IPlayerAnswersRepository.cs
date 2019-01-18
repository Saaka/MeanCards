using System.Collections.Generic;
using System.Threading.Tasks;
using MeanCards.Model.DAL.Creation.Players;
using MeanCards.Model.DAL.Modification.Players;
using MeanCards.Model.DTO.Players;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IPlayerAnswersRepository
    {
        Task<int> CreatePlayerAnswer(CreatePlayerAnswerModel model);
        Task<List<PlayerAnswerModel>> GetAllPlayerAnswers(int gameRoundId);
        Task<int> GetNumberOfAnswers(int gameRoundId);
        Task<bool> MarkAnswerAsSelected(int answerId);
        Task<bool> HasPlayerSubmittedAnswer(int playerId, int gameRoundId);
        Task<bool> IsAnswerSubmitted(int playerAnswerId, int gameRoundId);
        Task<bool> IsAnsweringPlayerActive(int playerAnswerId, int gameRoundId);
        Task<AddPlayerPointResult> AddPointForAnswer(int playerAnswerId, int gameRoundId);
    }
}
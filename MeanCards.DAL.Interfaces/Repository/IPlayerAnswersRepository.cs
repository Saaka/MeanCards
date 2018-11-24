using System.Collections.Generic;
using System.Threading.Tasks;
using MeanCards.Model.DAL.Creation.Players;
using MeanCards.Model.DTO.Players;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IPlayerAnswersRepository
    {
        Task<int> CreatePlayerAnswer(CreatePlayerAnswerModel model);
        Task<List<PlayerAnswerModel>> GetAllPlayerAnswers(int gameRoundId);
        Task MarkAnswerAsSelected(int answerId);
    }
}
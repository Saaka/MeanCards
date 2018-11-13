using System.Collections.Generic;
using System.Threading.Tasks;
using MeanCards.DataModel.Entity;
using MeanCards.Model.Creation;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IPlayerAnswersRepository
    {
        Task<int> CreatePlayerAnswer(CreatePlayerAnswerModel model);
        Task<List<PlayerAnswer>> GetAllPlayerAnswers(int gameRoundId);
        Task MarkAnswerAsSelected(int answerId);
    }
}
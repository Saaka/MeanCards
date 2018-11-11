using MeanCards.DataModel.Entity;
using MeanCards.Model.Creation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IQuestionCardsRepository
    {
        Task<int> CreateQuestionCard(CreateQuestionCardModel model);
        Task<List<QuestionCard>> GetAllActiveQuestionCards();
        Task<List<QuestionCard>> GetQuestionCardsWithoutMatureContent();
    }
}

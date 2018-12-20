using MeanCards.Model.DAL.Creation.QuestionCards;
using MeanCards.Model.DTO.QuestionCards;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IQuestionCardsRepository
    {
        Task<int> CreateQuestionCard(CreateQuestionCardModel model);
        Task CreateQuestionCards(List<CreateQuestionCardModel> models);
        Task<List<QuestionCardModel>> GetAllActiveQuestionCards();
        Task<List<QuestionCardModel>> GetQuestionCardsWithoutMatureContent();
        Task<QuestionCardModel> GetRoundQuestionCard(int gameRoundId);
        Task<bool> IsQuestionCardMultiChoice(int gameRoundId);
        Task<QuestionCardModel> GetRandomQuestionCardForGame(int gameId);
    }
}

using MeanCards.Model.DAL.Creation.AnswerCards;
using MeanCards.Model.DTO.AnswerCards;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IAnswerCardsRepository
    {
        Task<int> CreateAnswerCard(CreateAnswerCardModel model);
        Task CreateAnswerCards(List<CreateAnswerCardModel> models);
        Task<List<AnswerCardModel>> GetAllActiveAnswerCards();
        Task<List<AnswerCardModel>> GetAnswerCardsWithoutMatureContent();
        Task<List<AnswerCardModel>> GetRandomAnswerCardsForGame(int gameId, int cardCount);
    }
}

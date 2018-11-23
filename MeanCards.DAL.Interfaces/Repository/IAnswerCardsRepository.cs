using MeanCards.Model.Creation;
using MeanCards.Model.DTO.AnswerCards;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IAnswerCardsRepository
    {
        Task<int> CreateAnswerCard(CreateAnswerCardModel model);
        Task<List<AnswerCardModel>> GetAllActiveAnswerCards();
        Task<List<AnswerCardModel>> GetAnswerCardsWithoutMatureContent();
    }
}

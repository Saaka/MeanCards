using MeanCards.DataModel.Entity;
using MeanCards.Model.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeanCards.DAL.Interfaces.Repository
{
    public interface IAnswerCardsRepository
    {
        Task<int> CreateAnswerCard(CreateAnswerCardModel model);
        Task<List<AnswerCard>> GetAllActiveAnswerCards();
        Task<List<AnswerCard>> GetAnswerCardsWithoutMatureContent();
    }
}

using MeanCards.DAL.Interfaces.Initializer;
using MeanCards.DAL.Interfaces.Repository;
using System.Threading.Tasks;

namespace MeanCards.DAL.Initializer
{
    public class AnswerCardsInitializer : IAnswerCardsInitializer
    {
        private readonly IAnswerCardsRepository answerCardsRepository;
        private readonly ILanguagesRepository languagesRepository;

        public AnswerCardsInitializer(IAnswerCardsRepository answerCardsRepository,
            ILanguagesRepository languagesRepository)
        {
            this.answerCardsRepository = answerCardsRepository;
            this.languagesRepository = languagesRepository;
        }

        public async Task Seed()
        {

        }
    }
}

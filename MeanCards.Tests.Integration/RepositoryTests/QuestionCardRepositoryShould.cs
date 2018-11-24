using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.DAL.Creation.QuestionCards;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.RepositoryTests
{
    public class QuestionCardRepositoryShould : BaseRepositoryTests
    {
        [Fact]
        public async Task CreateQuestionCards()
        {
            var languageId = await Fixture.CreateDefaultLanguage();
            await PopulateQuestionCards(languageId);
            var cardRepository = Fixture.GetService<IQuestionCardsRepository>();

            var cards = await cardRepository.GetAllActiveQuestionCards();

            Assert.Equal(2, cards.Count);
        }

        [Fact]
        public async Task ReturnCardsWithoutMatureContent()
        {
            var languageId = await Fixture.CreateDefaultLanguage();
            await PopulateQuestionCards(languageId);
            var cardRepository = Fixture.GetService<IQuestionCardsRepository>();

            var cards = await cardRepository.GetQuestionCardsWithoutMatureContent();

            Assert.Single(cards);

            var card = cards.First();
            Assert.Equal("Test2", card.Text);
        }

        private async Task PopulateQuestionCards(int languageId)
        {
            var cardRepository = Fixture.GetService<IQuestionCardsRepository>();
            await cardRepository.CreateQuestionCard(new CreateQuestionCardModel
            {
                IsAdultContent = true,
                LanguageId = languageId,
                NumberOfAnswers = 1,
                Text = "Test1"
            });
            await cardRepository.CreateQuestionCard(new CreateQuestionCardModel
            {
                IsAdultContent = false,
                LanguageId = languageId,
                NumberOfAnswers = 1,
                Text = "Test2"
            });
        }
    }
}

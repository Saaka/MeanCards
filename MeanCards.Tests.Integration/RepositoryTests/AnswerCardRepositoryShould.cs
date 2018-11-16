using MeanCards.DAL.Interfaces.Repository;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.RepositoryTests
{
    public class AnswerCardRepositoryShould : BaseRepositoryTests
    {
        [Fact]
        public async Task InsertAnswerCardsCards()
        {
            var languageId = await Fixture.CreateDefaultLanguage();
            await PopulateAnswerCards(languageId);
            var cardRepository = Fixture.GetService<IAnswerCardsRepository>();

            var cards = await cardRepository.GetAllActiveAnswerCards();

            Assert.Equal(2, cards.Count);
        }

        [Fact]
        public async Task ReturnCardsWithoutMatureContent()
        {
            var languageId = await Fixture.CreateDefaultLanguage();
            await PopulateAnswerCards(languageId);
            var cardRepository = Fixture.GetService<IAnswerCardsRepository>();

            var cards = await cardRepository.GetAnswerCardsWithoutMatureContent();

            Assert.Single(cards);

            var card = cards.First();
            Assert.Equal("Test2", card.Text);
            Assert.True(card.IsActive);
        }

        private async Task PopulateAnswerCards(int languageId)
        {
            var cardRepository = Fixture.GetService<IAnswerCardsRepository>();
            await cardRepository.CreateAnswerCard(new Model.Creation.CreateAnswerCardModel
            {
                IsAdultContent = true,
                LanguageId = languageId,
                Text = "Test1"
            });
            await cardRepository.CreateAnswerCard(new Model.Creation.CreateAnswerCardModel
            {
                IsAdultContent = false,
                LanguageId = languageId,
                Text = "Test2"
            });
        }
    }
}

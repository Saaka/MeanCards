using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.DAL.Creation.AnswerCards;
using MeanCards.Tests.Integration.BaseTests;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.RepositoryTests
{
    public class AnswerCardRepositoryShould : BaseRepositoryTests
    {
        [Fact]
        public async Task CreateAnswerCards()
        {
            var languageId = await Fixture.CreateDefaultLanguage();
            await PopulateAnswerCards(languageId);
            var cardRepository = Fixture.GetService<IAnswerCardsRepository>();

            var cards = await cardRepository.GetAllActiveAnswerCards();

            Assert.Equal(2, cards.Count);
            TestHelper.AssertNumberOfFields<CreateAnswerCardModel>(3);
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
        }

        [Fact]
        public async Task GetRandomCardsForGameWithoutAdultContent()
        {
            var languageId = await Fixture.CreateDefaultLanguage();
            var englishLanguageId = await Fixture.CreateDefaultLanguage(
                code: "EN",
                name: "English");
            var userId = await Fixture.CreateDefaultUser();
            var gameId = await Fixture.CreateDefaultGame(
                languageId: languageId, 
                userId: userId,
                showAdultContent: false);

            await PopulateWithRandomCards(languageId);
            await PopulateWithRandomCards(englishLanguageId);

            var cardRepository = Fixture.GetService<IAnswerCardsRepository>();

            var cards = await cardRepository.GetRandomAnswerCardsForGame(gameId, 10);

            Assert.Equal(10, cards.Count);
            Assert.All(cards, c =>
            {
                Assert.False(c.IsAdultContent);
            });
            Assert.All(cards, c =>
            {
                Assert.Equal(languageId, c.LanguageId);
            });
        }

        private async Task PopulateWithRandomCards(int languageId)
        {
            var cards = new List<CreateAnswerCardModel>();
            for (int i = 0; i < 200; i++)
            {
                cards.Add(new CreateAnswerCardModel
                {
                    IsAdultContent = i % 2 == 1,
                    LanguageId = languageId,
                    Text = $"Test{i}"
                });
            }
            var cardRepository = Fixture.GetService<IAnswerCardsRepository>();

            await cardRepository.CreateAnswerCards(cards);
        }

        private async Task PopulateAnswerCards(int languageId)
        {
            var cardRepository = Fixture.GetService<IAnswerCardsRepository>();
            await cardRepository.CreateAnswerCard(new CreateAnswerCardModel
            {
                IsAdultContent = true,
                LanguageId = languageId,
                Text = "Test1"
            });
            await cardRepository.CreateAnswerCard(new CreateAnswerCardModel
            {
                IsAdultContent = false,
                LanguageId = languageId,
                Text = "Test2"
            });
        }
    }
}

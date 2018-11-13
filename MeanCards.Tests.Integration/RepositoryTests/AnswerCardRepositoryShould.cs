using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Tests.Integration.Config;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.RepositoryTests
{
    public class QuestionCardRepositoryShould : IDisposable
    {
        private readonly DALServiceCollectionFixture Fixture;
        const string DefaultLanguageCode = "PL";

        public QuestionCardRepositoryShould()
        {
            Fixture = new DALServiceCollectionFixture();
        }

        [Fact]
        public async Task InsertQuestionCards()
        {
            var languageId = await CreateDefaultLanguage();
            await PopulateQuestionCards(languageId);
            var cardRepository = Fixture.GetService<IAnswerCardsRepository>();

            var cards = await cardRepository.GetAllActiveAnswerCards();

            Assert.Equal(2, cards.Count);
        }

        [Fact]
        public async Task ReturnCardsWithoutMatureContent()
        {
            var languageId = await CreateDefaultLanguage();
            await PopulateQuestionCards(languageId);
            var cardRepository = Fixture.GetService<IAnswerCardsRepository>();

            var cards = await cardRepository.GetAnswerCardsWithoutMatureContent();

            Assert.Single(cards);

            var card = cards.First();
            Assert.Equal("Test2", card.Text);
            Assert.True(card.IsActive);
        }

        private async Task PopulateQuestionCards(int languageId)
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

        private async Task<int> CreateDefaultLanguage()
        {
            var languageRepository = Fixture.GetService<ILanguagesRepository>();
            return await languageRepository.CreateLanguage(new Model.Creation.CreateLanguageModel { Code = DefaultLanguageCode, Name = "Polski " });
        }

        public void Dispose()
        {
            Fixture.Dispose();
        }
    }
}

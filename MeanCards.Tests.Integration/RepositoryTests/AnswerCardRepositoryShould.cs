using MeanCards.DAL.Repository;
using MeanCards.DAL.Storage;
using MeanCards.Tests.Integration.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.RepositoryTests
{
    public class QuestionCardRepositoryShould
    {
        readonly DbContextOptions<AppDbContext> contextOptions;
        const string DefaultLanguageCode = "PL";

        public QuestionCardRepositoryShould()
        {
            contextOptions = TestInMemoryDbOptionsProvider.CreateOptions<AppDbContext>();
        }

        [Fact]
        public async Task InsertQuestionCards()
        {
            var languageId = await CreateDefaultLanguage();

            await PopulateQuestionCards(languageId);

            using (var context = new AppDbContext(contextOptions))
            {
                var cardRepository = new QuestionCardsRepository(context);

                var cards = await cardRepository.GetAllActiveQuestionCards();

                Assert.Equal(2, cards.Count);
            }
        }

        [Fact]
        public async Task ReturnCardsWithoutMatureContent()
        {
            var languageId = await CreateDefaultLanguage();

            await PopulateQuestionCards(languageId);

            using (var context = new AppDbContext(contextOptions))
            {
                var cardRepository = new QuestionCardsRepository(context);

                var cards = await cardRepository.GetQuestionCardsWithoutMatureContent();

                Assert.Single(cards);
            }
        }

        private async Task PopulateQuestionCards(int languageId)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                var cardRepository = new QuestionCardsRepository(context);

                await cardRepository.CreateQuestionCard(new Model.ViewModel.CreateQuestionCardModel
                {
                    IsAdultContent = true,
                    LanguageId = languageId,
                    NumberOfAnswers = 1,
                    Text = "Test1"
                });
                await cardRepository.CreateQuestionCard(new Model.ViewModel.CreateQuestionCardModel
                {
                    IsAdultContent = false,
                    LanguageId = languageId,
                    NumberOfAnswers = 1,
                    Text = "Test2"
                });
            }
        }

        private async Task<int> CreateDefaultLanguage()
        {
            using (var context = new AppDbContext(contextOptions))
            {
                var languageRepository = new LanguagesRepository(context);
                return await languageRepository.CreateLanguage(new Model.ViewModel.CreateLanguageModel { Code = DefaultLanguageCode, Name = "Polski " });
            }
        }
    }
}

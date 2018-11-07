using MeanCards.DAL.Repository;
using MeanCards.DAL.Storage;
using MeanCards.Tests.Integration.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.RepositoryTests
{
    public class AnswerCardRepositoryShould
    {
        readonly DbContextOptions<AppDbContext> contextOptions;
        const string DefaultLanguageCode = "PL";

        public AnswerCardRepositoryShould()
        {
            contextOptions = TestInMemoryDbOptionsProvider.CreateOptions<AppDbContext>();
        }

        [Fact]
        public async Task InsertAnswerCards()
        {
            var languageId = await CreateDefaultLanguage();

            await PopulateAnswerCards(languageId);

            using (var context = new AppDbContext(contextOptions))
            {
                var cardRepository = new AnswerCardsRepository(context);

                var cards = await cardRepository.GetAllActiveAnswerCards();

                Assert.Equal(2, cards.Count);
            }
        }

        [Fact]
        public async Task ReturnCardsWithoutMatureContent()
        {
            var languageId = await CreateDefaultLanguage();

            await PopulateAnswerCards(languageId);

            using (var context = new AppDbContext(contextOptions))
            {
                var cardRepository = new AnswerCardsRepository(context);

                var cards = await cardRepository.GetAnswerCardsWithoutMatureContent();

                Assert.Single(cards);
            }
        }

        private async Task PopulateAnswerCards(int languageId)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                var cardRepository = new AnswerCardsRepository(context);

                await cardRepository.CreateAnswerCard(new Model.ViewModel.CreateAnswerCardModel
                {
                    IsAdultContent = true,
                    LanguageId = languageId,
                    Text = "Test1"
                });
                await cardRepository.CreateAnswerCard(new Model.ViewModel.CreateAnswerCardModel
                {
                    IsAdultContent = false,
                    LanguageId = languageId,
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

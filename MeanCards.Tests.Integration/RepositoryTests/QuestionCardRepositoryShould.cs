using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.DAL.Creation.Games;
using MeanCards.Model.DAL.Creation.QuestionCards;
using MeanCards.Tests.Integration.BaseTests;
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
            TestHelper.AssertNumberOfFields<CreateQuestionCardModel>(4);
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

        [Fact]
        public async Task ReturnSingleRandomQuestionCard()
        {
            var languageId = await Fixture.CreateDefaultLanguage();
            var cardRepository = Fixture.GetService<IQuestionCardsRepository>();
            var userId = await Fixture.CreateDefaultUser();
            var gameId = await CreateGame(languageId, userId);

            var questionCardId = await cardRepository.CreateQuestionCard(new CreateQuestionCardModel
            {
                IsAdultContent = true,
                LanguageId = languageId,
                NumberOfAnswers = 1,
                Text = "Test1"
            });

            var questionCard = await cardRepository.GetRandomQuestionCardForGame(gameId);

            Assert.NotNull(questionCard);
            Assert.Equal(questionCardId, questionCard.QuestionCardId);
        }

        [Fact]
        public async Task NotReturnRandomQuestionCardWhenAllAreUsed()
        {
            var languageId = await Fixture.CreateDefaultLanguage();
            var cardRepository = Fixture.GetService<IQuestionCardsRepository>();
            var userId = await Fixture.CreateDefaultUser();
            var gameId = await CreateGame(languageId, userId);

            var questionCardId = await cardRepository.CreateQuestionCard(new CreateQuestionCardModel
            {
                IsAdultContent = true,
                LanguageId = languageId,
                NumberOfAnswers = 1,
                Text = "Test1"
            });
            await CreateGameRound(gameId, userId, questionCardId);

            var questionCard = await cardRepository.GetRandomQuestionCardForGame(gameId);

            Assert.Null(questionCard);
        }

        [Fact]
        public async Task ReturnSingleRandomQuestionCardWithoutMatureContent()
        {
            var languageId = await Fixture.CreateDefaultLanguage();
            var cardRepository = Fixture.GetService<IQuestionCardsRepository>();
            var userId = await Fixture.CreateDefaultUser();
            var gameId = await CreateGame(languageId, userId, false);

            await cardRepository.CreateQuestionCard(new CreateQuestionCardModel
            {
                IsAdultContent = true,
                LanguageId = languageId,
                NumberOfAnswers = 1,
                Text = "Test1"
            });
            var questionCardId = await cardRepository.CreateQuestionCard(new CreateQuestionCardModel
            {
                IsAdultContent = false,
                LanguageId = languageId,
                NumberOfAnswers = 1,
                Text = "Test1"
            });

            var questionCard = await cardRepository.GetRandomQuestionCardForGame(gameId);

            Assert.NotNull(questionCard);
            Assert.Equal(questionCardId, questionCard.QuestionCardId);
        }

        private async Task<int> CreateGame(int languageId, int userId, bool showAdultContent = true)
        {
            var gameId = await Fixture.CreateDefaultGame(languageId, userId, showAdultContent: showAdultContent);

            return gameId;
        }

        private async Task CreateGameRound(int gameId, int userId, int questionCardId)
        {
            var repo = Fixture.GetService<IGameRoundsRepository>();

            var playerId = await Fixture.CreateDefaultPlayer(gameId, userId);
            await repo.CreateGameRound(new CreateGameRoundModel
            {
                GameId = gameId,
                QuestionCardId = questionCardId,
                RoundNumber = 1,
                OwnerPlayerId = playerId
            });
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

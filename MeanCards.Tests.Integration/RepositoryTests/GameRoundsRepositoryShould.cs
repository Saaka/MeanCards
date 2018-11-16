using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Creation;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.RepositoryTests
{
    public class GameRoundsRepositoryShould : BaseRepositoryTests
    {
        [Fact]
        public async Task CreateGameRound()
        {
            var languageId = await CreateDefaultLanguage();
            var userId = await CreateDefaultUser();
            var gameId = await CreateDefaultGame(languageId, userId);
            var playerId = await CreateDefaultPlayer(userId, gameId);
            var questionCardId = await CreateQuestionCard(languageId);

            var repository = Fixture.GetService<IGameRoundsRepository>();
            var createRound = new CreateGameRoundModel
            {
                GameId = gameId,
                RoundOwnerId = playerId,
                QuestionCardId = questionCardId
            };

            var gameRoundId = await repository.CreateGameRound(createRound);
            var gameRound = await repository.GetCurrentGameRound(gameId);

            Assert.NotNull(gameRound);
            Assert.Equal(playerId, gameRound.RoundOwnerId);
            Assert.Equal(gameId, gameRound.GameId);
            Assert.Equal(questionCardId, gameRound.QuestionCardId);
            Assert.True(gameRound.IsActive);
        }

        private async Task<int> CreateQuestionCard(int languageId)
        {
            var repository = Fixture.GetService<IQuestionCardsRepository>();

            return await repository.CreateQuestionCard(new CreateQuestionCardModel
            {
                LanguageId = languageId,
                NumberOfAnswers = 1,
                Text = "Test",
                IsAdultContent = false
            });
        }
    }
}

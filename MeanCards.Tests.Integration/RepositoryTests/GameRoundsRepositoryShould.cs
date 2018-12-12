using MeanCards.Common.Enums;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.DAL.Creation.Games;
using MeanCards.Model.DAL.Creation.QuestionCards;
using MeanCards.Tests.Integration.BaseTests;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.RepositoryTests
{
    public class GameRoundsRepositoryShould : BaseRepositoryTests
    {
        [Fact]
        public async Task CreateGameRound()
        {
            var languageId = await Fixture.CreateDefaultLanguage();
            var userId = await Fixture.CreateDefaultUser();
            var gameId = await Fixture.CreateDefaultGame(languageId, userId);
            var playerId = await Fixture.CreateDefaultPlayer(userId, gameId);
            var questionCardId = await CreateQuestionCard(languageId);

            var repository = Fixture.GetService<IGameRoundsRepository>();
            var createRound = new CreateGameRoundModel
            {
                GameId = gameId,
                RoundOwnerId = playerId,
                QuestionCardId = questionCardId,
                RoundNumber = 1
            };

            var gameRoundId = await repository.CreateGameRound(createRound);
            var gameRound = await repository.GetCurrentGameRound(gameId);

            Assert.NotNull(gameRound);
            Assert.Equal(playerId, gameRound.OwnerId);
            Assert.Equal(gameId, gameRound.GameId);
            Assert.Equal(questionCardId, gameRound.QuestionCardId);
            Assert.Equal(1, gameRound.Number);
            Assert.Equal(GameRoundStatusEnum.Pending, gameRound.Status);
            TestHelper.AssertNumberOfFields<CreateGameRoundModel>(4);
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

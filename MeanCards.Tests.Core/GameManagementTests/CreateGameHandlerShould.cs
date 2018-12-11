using MeanCards.Common.Constants;
using MeanCards.GameManagement;
using MeanCards.Model.Core.Games;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Core.GameManagementTests
{
    public class CreateGameHandlerShould : BaseCoreTests
    {
        [Fact]
        public async Task CreateGameForValidScenario()
        {
            var userId = await Fixture.CreateDefaultUser();
            var languageId = await Fixture.CreateDefaultLanguage();
            await Fixture.CreateQuestionCards(languageId, 1);
            await Fixture.CreateAnswerCards(languageId);

            var handler = Fixture.GetService<ICreateGameHandler>();

            var result = await handler.Handle(new CreateGame
            {
                LanguageId = languageId,
                Name = "TestGame",
                OwnerId = userId,
                ShowAdultContent = false
            });

            Assert.NotNull(result);
            Assert.True(result.IsSuccessful);
            Assert.NotEqual(0, result.GameId);
            Assert.NotNull(result.Code);
            Assert.NotNull(result.Checkpoint);
            TestHelper.AssertNumberOfFields<CreateGameResult>(6);
        }

        [Fact]
        public async Task NotCreateGameForMissingAnswerCards()
        {
            var userId = await Fixture.CreateDefaultUser();
            var languageId = await Fixture.CreateDefaultLanguage();
            await Fixture.CreateQuestionCards(languageId, 1);
            await Fixture.CreateAnswerCards(languageId, 5);

            var handler = Fixture.GetService<ICreateGameHandler>();

            var result = await handler.Handle(new CreateGame
            {
                LanguageId = languageId,
                Name = "TestGame",
                OwnerId = userId,
                ShowAdultContent = false
            });

            Assert.NotNull(result);
            Assert.False(result.IsSuccessful);
            Assert.Equal(GameErrors.NotEnoughAnswerCards, result.Error);
            Assert.Equal(0, result.GameId);
            Assert.Null(result.Code);
        }

        [Fact]
        public async Task NotCreateGameForMissingQuestionCards()
        {
            var userId = await Fixture.CreateDefaultUser();
            var languageId = await Fixture.CreateDefaultLanguage();

            var handler = Fixture.GetService<ICreateGameHandler>();

            var result = await handler.Handle(new CreateGame
            {
                LanguageId = languageId,
                Name = "TestGame",
                OwnerId = userId,
                ShowAdultContent = false
            });

            Assert.NotNull(result);
            Assert.False(result.IsSuccessful);
            Assert.Equal(GameErrors.NoQuestionCardsAvailable, result.Error);
            Assert.Equal(0, result.GameId);
            Assert.Null(result.Code);
        }
    }
}

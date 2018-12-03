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
        }
    }
}

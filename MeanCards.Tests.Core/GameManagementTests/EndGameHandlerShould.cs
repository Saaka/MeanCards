using MeanCards.GameManagement;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Core.GameManagementTests
{
    [Collection(TestCollections.GameHandlers)]
    public class EndGameHandlerShould : BaseCoreTests
    {
        [Fact]
        public async Task EndGameWithPendingStatus()
        {
            var game = await Fixture.CreateGame();

            var handler = Fixture.GetService<IEndGameHandler>();

            var request = new Model.Core.Games.EndGame
            {
                GameId = game.GameId,
                UserId = game.OwnerId
            };

            var result = await handler.Handle(request);

            Assert.NotNull(result);
            Assert.True(result.IsSuccessful);
            Assert.Null(result.Error);
        }
    }
}

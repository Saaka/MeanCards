using MeanCards.GameManagement;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Core.GameManagementTests
{
    [Collection(TestCollections.GameHandlers)]
    public class CancelGameHandlerShould : BaseCoreTests
    {
        [Fact]
        public async Task CancelGameWithPendingStatus()
        {
            var game = await Fixture.CreateGame();

            var handler = Fixture.GetService<ICancelGameHandler>();

            var request = new Model.Core.Games.CancelGame
            {
                GameId = game.GameId,
                UserId = game.OwnerId
            };

            var result = await handler.Handle(request, new System.Threading.CancellationToken());

            Assert.NotNull(result);
            Assert.True(result.IsSuccessful);
            Assert.Null(result.Error);
        }
    }
}

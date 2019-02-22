using MeanCards.GameManagement;
using MeanCards.Model.Core.Games;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Core.GameManagementTests
{
    [Collection(TestCollections.GameHandlers)]
    public class JoinGameHandlerShould : BaseCoreTests
    { 
        [Fact]
        public async Task AllowNewPlayerToJoinGame()
        {
            var game = await Fixture.CreateGame();
            var newUserId = await Fixture.CreateDefaultUser(
                userName: "Nowak",
                email: "test2@test.com",
                userCode: "123456");

            var joinGameHandler = Fixture.GetService<IJoinGameHandler>();

            var request = new JoinGame
            {
                GameId = game.GameId,
                UserId = newUserId
            };

            var result = await joinGameHandler.Handle(request, new System.Threading.CancellationToken());

            Assert.True(result.IsSuccessful);
            Assert.NotEqual(0, result.PlayerId);
        }

        [Fact]
        public async Task FailForPlayerAlreadyInTheGame()
        {
            var game = await Fixture.CreateGame();

            var joinGameHandler = Fixture.GetService<IJoinGameHandler>();

            var request = new JoinGame
            {
                GameId = game.GameId,
                UserId = game.OwnerId
            };

            var result = await joinGameHandler.Handle(request, new System.Threading.CancellationToken());

            Assert.False(result.IsSuccessful);
            Assert.Equal(0, result.PlayerId);
        }
    }
}

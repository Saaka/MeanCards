using MeanCards.GameManagement;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Core.GameManagementTests
{
    public class StartGameRoundHandlerShould : BaseCoreTests
    {
        [Fact]
        public async Task StartRoundForValidData()
        {
            var game = await Fixture.CreateGame();
            var round = await Fixture.GetCurrentGameRound(game.GameId);
            var handler = Fixture.GetService<IStartGameRoundHandler>();

            var request = new Model.Core.Games.StartGameRound
            {
                GameId = game.GameId,
                GameRoundId = round.GameRoundId,
                UserId = game.OwnerId
            };

            var result = await handler.Handle(request);

            Assert.True(result.IsSuccessful);
            Assert.Null(result.Error);
        }
    }
}

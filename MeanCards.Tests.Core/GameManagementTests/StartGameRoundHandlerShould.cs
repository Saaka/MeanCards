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

        }
    }
}

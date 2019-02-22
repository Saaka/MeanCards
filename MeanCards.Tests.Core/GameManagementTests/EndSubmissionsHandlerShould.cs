using MeanCards.GameManagement;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Core.GameManagementTests
{
    [Collection(TestCollections.GameHandlers)]
    public class EndSubmissionsHandlerShould : BaseCoreTests
    {
        [Fact]
        public async Task EndSubmissionForValidData()
        {
            var game = await Fixture.CreateGame();
            var round = await Fixture.GetCurrentGameRound(game.GameId);
            await Fixture.StartGameRound(game.GameId, round.GameRoundId, game.OwnerId);
            await Fixture.SubmitAnswersForAllPlayers(game.GameId, round.GameRoundId, round.OwnerPlayerId);

            var request = new Model.Core.Games.EndSubmissions
            {
                GameId = game.GameId,
                GameRoundId = round.GameRoundId,
                UserId = game.OwnerId
            };

            var handler = Fixture.GetService<IEndSubmissionsHandler>();

            var result = await handler.Handle(request, new System.Threading.CancellationToken());

            Assert.True(result.IsSuccessful);
            Assert.Null(result.Error);
        }
    }
}

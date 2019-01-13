using MeanCards.Common.Enums;
using MeanCards.GameManagement;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Core.GameManagementTests
{
    [Collection(TestCollections.GameHandlers)]
    public class SkipRoundHandlerShould : BaseCoreTests
    {
        [Fact]
        public async Task SkipRoundForPendingRound()
        {
            var game = await Fixture.CreateGame();
            var round = await Fixture.GetCurrentGameRound(game.GameId);
            var handler = Fixture.GetService<ISkipRoundHandler>();
            
            var request = new Model.Core.Games.SkipRound
            {
                GameId = game.GameId,
                GameRoundId = round.GameRoundId,
                UserId = game.OwnerId
            };
            
            var result = await handler.Handle(request);

            Assert.True(result.IsSuccessful);
            Assert.Null(result.Error);

            var newRound = await Fixture.GetCurrentGameRound(game.GameId);

            Assert.NotEqual(round.OwnerPlayerId, newRound.OwnerPlayerId);
            Assert.NotEqual(round.GameRoundId, newRound.GameRoundId);
            Assert.Equal(GameRoundStatusEnum.Pending, round.Status);

            round = await Fixture.GetGameRound(game.GameId, round.GameRoundId);

            Assert.Equal(GameRoundStatusEnum.Skipped, round.Status);
        }

        [Fact]
        public async Task SkipRoundForStartedRound()
        {
            var game = await Fixture.CreateGame();
            var round = await Fixture.GetCurrentGameRound(game.GameId);
            await Fixture.StartGameRound(game.GameId, round.GameRoundId, game.OwnerId);
            await Fixture.SubmitAnswersForAllPlayers(game.GameId, round.GameRoundId, round.OwnerPlayerId);

            var handler = Fixture.GetService<ISkipRoundHandler>();

            var request = new Model.Core.Games.SkipRound
            {
                GameId = game.GameId,
                GameRoundId = round.GameRoundId,
                UserId = game.OwnerId
            };

            var result = await handler.Handle(request);

            Assert.True(result.IsSuccessful);
            Assert.Null(result.Error);

            var newRound = await Fixture.GetCurrentGameRound(game.GameId);

            Assert.NotEqual(round.OwnerPlayerId, newRound.OwnerPlayerId);
            Assert.NotEqual(round.GameRoundId, newRound.GameRoundId);
            Assert.Equal(GameRoundStatusEnum.Pending, round.Status);

            round = await Fixture.GetGameRound(game.GameId, round.GameRoundId);

            Assert.Equal(GameRoundStatusEnum.Skipped, round.Status);
        }
    }
}

using MeanCards.Common.Enums;
using MeanCards.GameManagement;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Core.GameManagementTests
{
    [Collection(TestCollections.GameHandlers)]
    public class SelectAnswerHandlerShould : BaseCoreTests
    {
        [Fact]
        public async Task SelectAnswerForFirstRound()
        {
            var game = await Fixture.CreateGame();
            var round = await Fixture.GetCurrentGameRound(game.GameId);
            var user = await Fixture.AddNewPlayerToGame(game.GameId);
            await Fixture.StartGameRound(game.GameId, round.GameRoundId, game.OwnerId);
            await Fixture.SubmitAnswersForAllPlayers(game.GameId, round.GameRoundId, round.OwnerPlayerId);
            await Fixture.EndSubmissions(game.GameId, round.GameRoundId, game.OwnerId);

            var playerAnswer = await Fixture.GetPlayerAnswer(round.GameRoundId, user.PlayerId);

            var request = new Model.Core.Games.SelectAnswer
            {
                GameId = game.GameId,
                GameRoundId = round.GameRoundId,
                UserId = game.OwnerId,
                PlayerAnswerId = playerAnswer.PlayerAnswerId
            };

            var handler = Fixture.GetService<ISelectAnswerHandler>();

            var result = await handler.Handle(request);

            Assert.True(result.IsSuccessful);
            Assert.Null(result.Error);

            var player = await Fixture.GetPlayer(user.PlayerId);

            Assert.Equal(1, player.Points);
        }

        [Fact]
        public async Task FinishGameForPointLimit()
        {
            int pointsLimit = 3;
            var game = await Fixture.CreateGame(
                additionalPlayersCount: 5,
                pointsLimit: pointsLimit);
            var user = await Fixture.AddNewPlayerToGame(game.GameId);
            
            var handler = Fixture.GetService<ISelectAnswerHandler>();
            for (int i = 0; i < pointsLimit; i++)
            {
                var round = await Fixture.GetCurrentGameRound(game.GameId);
                var ownerPlayer = await Fixture.GetPlayer(round.OwnerPlayerId);

                await Fixture.StartGameRound(game.GameId, round.GameRoundId, ownerPlayer.UserId);
                await Fixture.SubmitAnswersForAllPlayers(game.GameId, round.GameRoundId, round.OwnerPlayerId);
                await Fixture.EndSubmissions(game.GameId, round.GameRoundId, ownerPlayer.UserId);

                var playerAnswer = await Fixture.GetPlayerAnswer(round.GameRoundId, user.PlayerId);

                var request = new Model.Core.Games.SelectAnswer
                {
                    GameId = game.GameId,
                    GameRoundId = round.GameRoundId,
                    UserId = ownerPlayer.UserId,
                    PlayerAnswerId = playerAnswer.PlayerAnswerId
                };

                var result = await handler.Handle(request);

                Assert.True(result.IsSuccessful);
                Assert.Null(result.Error);
            }

            var player = await Fixture.GetPlayer(user.PlayerId);
            Assert.Equal(pointsLimit, player.Points);

            game = await Fixture.GetGame(game.GameId);

            Assert.Equal(player.UserId, game.WinnerId);
            Assert.Equal(GameStatusEnum.Finished, game.Status);
        }
    }
}

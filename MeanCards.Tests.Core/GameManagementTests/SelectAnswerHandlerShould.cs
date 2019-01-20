using MeanCards.GameManagement;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Core.GameManagementTests
{
    [Collection(TestCollections.GameHandlers)]
    public class SelectAnswerHandlerShould : BaseCoreTests
    {
        [Fact]
        public async Task SelectAnswer()
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
        }
    }
}

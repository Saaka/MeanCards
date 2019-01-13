using MeanCards.GameManagement;
using MeanCards.Model.Core.Games;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Core.GameManagementTests
{
    [Collection(TestCollections.GameHandlers)]
    public class SubmitAnswerHandlerShould : BaseCoreTests
    {
        [Fact]
        public async Task SubmitAnswerForValidData()
        {
            var game = await Fixture.CreateGame();
            var round = await Fixture.GetCurrentGameRound(game.GameId);
            var userId = await Fixture.CreateRandomUser();
            var player = await Fixture.JoinPlayer(game.GameId, userId);
            await Fixture.StartGameRound(game.GameId, round.GameRoundId, game.OwnerId);

            var cardId = await Fixture.GetRandomPlayerCard(player.PlayerId);
            var request = new SubmitAnswer
            {
                GameId = game.GameId,
                GameRoundId = round.GameRoundId,
                UserId = userId,
                PlayerCardId = cardId
            };

            var handler = Fixture.GetService<ISubmitAnswerHandler>();
            var result = await handler.Handle(request);

            Assert.True(result.IsSuccessful);
            Assert.Null(result.Error);
        }
    }
}

using MeanCards.Model.Core.Games;
using MeanCards.Tests.Unit.ValidatorTests.GamesTests.Mocks;
using MeanCards.Validators.Games;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Unit.ValidatorTests.GamesTests
{
    public class EndSubmissionsValidatorShould
    {
        [Fact]
        public async Task ReturnSuccessForValidData()
        {
            var baseMock = BaseGameRequestsValidatorMock.CreateMock();
            var playersRepo = PlayersRepositoryMock.Create().Object;
            var gameRoundRepo = GameRoundsRepositoryMock.Create(
                status: Common.Enums.GameRoundStatusEnum.InProgress).Object;
            var gameRepo = GamesRepositoryMock.Create().Object;
            var playerAnswersRepo = PlayerAnswersRepositoryMock.Create().Object;

            var validator = new EndSubmissionsValidator(baseMock.Object, playersRepo, gameRoundRepo, gameRepo,  playerAnswersRepo);

            var request = new EndSubmissions
            {
                GameRoundId = 1,
                UserId = 1,
                GameId = 1,
            };

            var result = await validator.Validate(request);

            Assert.True(result.IsSuccessful);
            baseMock.Verify(x => x.Validate(request));
        }
    }
}

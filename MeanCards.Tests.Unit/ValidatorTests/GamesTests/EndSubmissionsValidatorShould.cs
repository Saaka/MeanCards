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
            var gameRoundRepo = GameRoundsRepositoryMock.Create(
                status: Common.Enums.GameRoundStatusEnum.InProgress).Object;
            var playerAnswersRepo = PlayerAnswersRepositoryMock.Create().Object;
            var gameOrRoundOnwerRuleMock = GameOrRoundOwnerRuleMock.Create<EndSubmissions>();

            var validator = new EndSubmissionsValidator(baseMock.Object, gameRoundRepo,  playerAnswersRepo, gameOrRoundOnwerRuleMock.Object);

            var request = new EndSubmissions
            {
                GameRoundId = 1,
                UserId = 1,
                GameId = 1,
            };

            var result = await validator.Validate(request);

            Assert.True(result.IsSuccessful);
            baseMock.Verify(x => x.Validate(request));
            gameOrRoundOnwerRuleMock.Verify(x => x.Validate(request));
        }
    }
}

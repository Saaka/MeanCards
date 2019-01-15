using MeanCards.Common.Constants;
using MeanCards.Common.Enums;
using MeanCards.Model.Core.Games;
using MeanCards.Tests.Unit.ValidatorTests.GamesTests.Mocks;
using MeanCards.Validators.Games;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Unit.ValidatorTests.GamesTests
{
    public class SkipRoundValidatorShould
    {
        [Theory]
        [InlineData(GameRoundStatusEnum.Pending)]
        [InlineData(GameRoundStatusEnum.InProgress)]
        [InlineData(GameRoundStatusEnum.Selection)]
        public async Task ReturnSuccessForValidData(GameRoundStatusEnum gameRoundStatus)
        {
            var baseMock = BaseGameRequestsValidatorMock.CreateMock();
            var gameRoundRepoMock = GameRoundsRepositoryMock.Create(
                status: gameRoundStatus);
            var gameOwnerRuleMock = GameOrRoundOwnerRuleMock.Create<SkipRound>();

            var validator = new SkipRoundValidator(baseMock.Object, gameRoundRepoMock.Object, gameOwnerRuleMock.Object);

            var request = new SkipRound
            {
                GameId = 1,
                UserId = 1,
                GameRoundId = 1
            };

            var result = await validator.Validate(request);

            Assert.True(result.IsSuccessful);
            baseMock.Verify(x => x.Validate(request));
            gameOwnerRuleMock.Verify(x => x.Validate(request));
        }

        [Theory]
        [InlineData(GameRoundStatusEnum.Cancelled)]
        [InlineData(GameRoundStatusEnum.Finished)]
        [InlineData(GameRoundStatusEnum.Skipped)]
        public async Task ReturnFailureForInvalidGameStatus(GameRoundStatusEnum gameRoundStatus)
        {
            var baseMock = BaseGameRequestsValidatorMock.CreateMock();
            var gameRoundRepoMock = GameRoundsRepositoryMock.Create(
                status: gameRoundStatus);
            var gameOwnerRuleMock = GameOrRoundOwnerRuleMock.Create<SkipRound>();

            var validator = new SkipRoundValidator(baseMock.Object, gameRoundRepoMock.Object, gameOwnerRuleMock.Object);

            var request = new SkipRound
            {
                GameId = 1,
                UserId = 1,
                GameRoundId = 1
            };

            var result = await validator.Validate(request);

            Assert.NotNull(result);
            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.InvalidGameRoundStatus, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForInactiveRound()
        {
            var baseMock = BaseGameRequestsValidatorMock.CreateMock();
            var gameRoundRepoMock = GameRoundsRepositoryMock.Create(
                isRoundActive: false);
            var gameOwnerRuleMock = GameOrRoundOwnerRuleMock.Create<SkipRound>();

            var validator = new SkipRoundValidator(baseMock.Object, gameRoundRepoMock.Object, gameOwnerRuleMock.Object);

            var request = new SkipRound
            {
                GameId = 1,
                UserId = 1,
                GameRoundId = 1
            };

            var result = await validator.Validate(request);

            Assert.NotNull(result);
            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.InvalidGameRoundStatus, result.Error);
        }
    }
}

using MeanCards.Common.Constants;
using MeanCards.Model.Core.Games;
using MeanCards.Tests.Unit.ValidatorTests.GamesTests.Mocks;
using MeanCards.Validators.Games;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Unit.ValidatorTests.GamesTests
{
    public class SelectAnswerValidatorShould
    {
        [Fact]
        public async Task ReturnSuccessForValidData()
        {
            var baseMock = BaseGameRequestsValidatorMock.CreateMock();
            var playerAnswersRepo = PlayerAnswersRepositoryMock.Create().Object;
            var roundOnwerRuleMock = RoundOwnerRuleMock.Create<SelectAnswer>();
            var gameRoundRepository = GameRoundsRepositoryMock.Create(
                status: Common.Enums.GameRoundStatusEnum.Selection).Object;

            var validator = new SelectAnswerValidator(baseMock.Object, roundOnwerRuleMock.Object, playerAnswersRepo, gameRoundRepository);

            var request = new SelectAnswer
            {
                GameRoundId = 1,
                UserId = 1,
                GameId = 1,
            };

            var result = await validator.Validate(request);

            Assert.True(result.IsSuccessful);
            baseMock.Verify(x => x.Validate(request));
            roundOnwerRuleMock.Verify(x => x.Validate(request));
        }

        [Fact]
        public async Task ReturnFailureForInvalidPlayerAnswer()
        {
            var baseMock = BaseGameRequestsValidatorMock.CreateMock();
            var playerAnswersRepo = PlayerAnswersRepositoryMock.Create(
                isAnswerSubmitted: false).Object;
            var roundOnwerRuleMock = RoundOwnerRuleMock.Create<SelectAnswer>();
            var gameRoundRepository = GameRoundsRepositoryMock.Create(
                status: Common.Enums.GameRoundStatusEnum.Selection).Object;

            var validator = new SelectAnswerValidator(baseMock.Object, roundOnwerRuleMock.Object, playerAnswersRepo, gameRoundRepository);

            var request = new SelectAnswer
            {
                GameRoundId = 1,
                UserId = 1,
                GameId = 1,
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.PlayerAnswerDoesNotExists, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForInvalidRoundStatus()
        {
            var baseMock = BaseGameRequestsValidatorMock.CreateMock();
            var playerAnswersRepo = PlayerAnswersRepositoryMock.Create().Object;
            var roundOnwerRuleMock = RoundOwnerRuleMock.Create<SelectAnswer>();
            var gameRoundRepository = GameRoundsRepositoryMock.Create(
                status: Common.Enums.GameRoundStatusEnum.InProgress).Object;

            var validator = new SelectAnswerValidator(baseMock.Object, roundOnwerRuleMock.Object, playerAnswersRepo, gameRoundRepository);

            var request = new SelectAnswer
            {
                GameRoundId = 1,
                UserId = 1,
                GameId = 1,
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.InvalidGameRoundStatus, result.Error);
        }
    }
}

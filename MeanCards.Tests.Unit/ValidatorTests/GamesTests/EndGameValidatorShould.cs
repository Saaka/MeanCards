﻿using MeanCards.Common.Constants;
using MeanCards.Model.Core.Games;
using MeanCards.Tests.Unit.ValidatorTests.GamesTests.Mocks;
using MeanCards.Validators.Games;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Unit.ValidatorTests.GamesTests
{
    public class EndGameValidatorShould
    {
        [Fact]
        public async Task ReturnSuccessForValidData()
        {
            var baseMock = BaseGameRequestsValidatorMock.CreateMock();
            var gameRepoMock = GamesRepositoryMock.Create();
            var gameOwnerRuleMock = GameOwnerRuleMock.Create<EndGame>();

            var validator = new EndGameValidator(baseMock.Object, gameOwnerRuleMock.Object, gameRepoMock.Object);

            var request = new EndGame
            {
                GameId = 1,
                UserId = 1
            };

            var result = await validator.Validate(request);

            Assert.True(result.IsSuccessful);
            baseMock.Verify(x => x.Validate(request));
            gameOwnerRuleMock.Verify(x => x.Validate(request));
        }
        
        [Theory]
        [InlineData(Common.Enums.GameStatusEnum.Canceled)]
        [InlineData(Common.Enums.GameStatusEnum.Finished)]
        public async Task ReturnFailureForInvalidStatus(Common.Enums.GameStatusEnum gameStatus)
        {
            var baseMock = BaseGameRequestsValidatorMock.CreateMock();
            var gameRepoMock = GamesRepositoryMock.Create(
                gameStatus: gameStatus);
            var gameOwnerRuleMock = GameOwnerRuleMock.Create<EndGame>();

            var validator = new EndGameValidator(baseMock.Object, gameOwnerRuleMock.Object, gameRepoMock.Object);

            var request = new EndGame
            {
                GameId = 1,
                UserId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.GameNotFoundOrInactive, result.Error);
        }
    }
}

using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games.Base;
using MeanCards.Model.DTO.Games;
using MeanCards.Validators.Games.Base;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Unit.ValidatorTests.GamesTests.BaseTests
{
    public class GameRoundRequestValidatorShould
    {
        [Fact]
        public async Task ReturnSuccessResultForValidData()
        {
            var gameRoundRepo = CreateGameRoundRepoMock();
            var validator = new GameRoundRequestValidator(gameRoundRepo);

            var request = new GameRoundRequestConcrete
            {
                GameId = 1,
                GameRoundId = 1
            };

            var result = await validator.Validate(request);

            Assert.True(result.IsSuccessful);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task ReturnFailureResultForMissingRoundId()
        {
            var gameRoundRepo = CreateGameRoundRepoMock();
            var validator = new GameRoundRequestValidator(gameRoundRepo);

            var request = new GameRoundRequestConcrete
            {
                GameId = 1,
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.GameRoundIdRequired, result.Error);
        }

        [Fact]
        public async Task ReturnFailureResultForNotExistingRound()
        {
            var gameRoundRepo = CreateGameRoundRepoMock(
                roundExists: false);
            var validator = new GameRoundRequestValidator(gameRoundRepo);

            var request = new GameRoundRequestConcrete
            {
                GameId = 1,
                GameRoundId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.RoundNotLinkedWithGame, result.Error);
        }

        [Fact]
        public async Task ReturnFailureResultForRoundNotLinkedWithGame()
        {
            var gameRoundRepo = CreateGameRoundRepoMock(
                isRoundInGame: false);
            var validator = new GameRoundRequestValidator(gameRoundRepo);

            var request = new GameRoundRequestConcrete
            {
                GameId = 1,
                GameRoundId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.RoundNotLinkedWithGame, result.Error);
        }

        [Fact]
        public async Task ReturnFailureResultForInactiveGameRound()
        {
            var gameRoundRepo = CreateGameRoundRepoMock(
                isActive: false);
            var validator = new GameRoundRequestValidator(gameRoundRepo);

            var request = new GameRoundRequestConcrete
            {
                GameId = 1,
                GameRoundId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.InvalidGameRoundStatus, result.Error);
        }

        private IGameRoundsRepository CreateGameRoundRepoMock(
            bool roundExists = true,
            bool isRoundInGame = true,
            bool isActive = true)
        {
            var mock = new Mock<IGameRoundsRepository>();
            mock.Setup(m => m.GetGameRound(It.IsAny<int>(), It.IsAny<int>())).Returns(() =>
            {
                if (!isRoundInGame || !roundExists)
                    return Task.FromResult<GameRoundModel>(null);

                return Task.FromResult(new GameRoundModel
                {
                    IsActive = isActive
                });
            });

            return mock.Object;
        }

        class GameRoundRequestConcrete : IGameRoundRequest
        {
            public int GameRoundId { get; set; }

            public int GameId { get; set; }
        }
    }
}

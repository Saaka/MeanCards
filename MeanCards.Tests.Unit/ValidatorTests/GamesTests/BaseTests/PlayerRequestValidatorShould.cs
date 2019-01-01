using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games.Base;
using MeanCards.Model.DTO.Players;
using MeanCards.Validators.Games.Base;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Unit.ValidatorTests.GamesTests.BaseTests
{
    public class PlayerRequestValidatorShould
    {
        [Fact]
        public async Task ReturnSuccessResultForValidData()
        {
            var playerRepo = CreatePlayersRepoMock();
            var validator = new PlayerRequestValidator(playerRepo);

            var request = new PlayerRequestConcrete
            {
                UserId = 1,
                GameId = 1
            };

            var result = await validator.Validate(request);

            Assert.True(result.IsSuccessful);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task ReturnFailureResultForMissingUserId()
        {
            var playerRepo = CreatePlayersRepoMock();
            var validator = new PlayerRequestValidator(playerRepo);

            var request = new PlayerRequestConcrete
            {
                GameId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.UserIdRequired, result.Error);
        }

        [Fact]
        public async Task ReturnFailureResultForMissingGameId()
        {
            var playerRepo = CreatePlayersRepoMock();
            var validator = new PlayerRequestValidator(playerRepo);

            var request = new PlayerRequestConcrete
            {
                UserId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.GameIdRequired, result.Error);
        }

        [Fact]
        public async Task ReturnFailureResultForNotExistingPlayer()
        {
            var playerRepo = CreatePlayersRepoMock(
                playerExists: false);
            var validator = new PlayerRequestValidator(playerRepo);

            var request = new PlayerRequestConcrete
            {
                UserId = 1,
                GameId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.PlayerNotFound, result.Error);
        }

        [Fact]
        public async Task ReturnFailureResultForInactivePlayer()
        {
            var playerRepo = CreatePlayersRepoMock(
                isPlayerActive: false);
            var validator = new PlayerRequestValidator(playerRepo);

            var request = new PlayerRequestConcrete
            {
                UserId = 1,
                GameId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.PlayerNotActive, result.Error);
        }

        private IPlayersRepository CreatePlayersRepoMock(
            bool playerExists = true,
            bool isPlayerActive = true)
        {
            var mock = new Mock<IPlayersRepository>();
            mock.Setup(m => m.GetPlayerByUserId(It.IsAny<int>(), It.IsAny<int>())).Returns(() =>
            {
                if (!playerExists)
                    return Task.FromResult<PlayerModel>(null);

                return Task.FromResult(new PlayerModel
                {
                    IsActive = isPlayerActive
                });
            });

            return mock.Object;
        }

        class PlayerRequestConcrete : IPlayerRequest
        {
            public int UserId { get; set; }
            public int GameId { get; set; }
        }
    }
}

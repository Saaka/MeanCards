using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games.Base;
using MeanCards.Model.DTO.Games;
using MeanCards.Validators.Games.Base;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Unit.ValidatorTests.GamesTests.BaseTests
{
    public class GameRequestValidatorShould
    {
        [Fact]
        public async Task ReturnSuccessResultForValidData()
        {
            var gameRepository = CreateGameRepositoryMock();
            var validator = new GameRequestValidator(gameRepository);

            var request = new GameRequestConcrete
            {
                GameId = 1
            };

            var result = await validator.Validate(request);

            Assert.True(result.IsSuccessful);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task ReturnFailureResultForMissingGameId()
        {
            var gameRepository = CreateGameRepositoryMock();
            var validator = new GameRequestValidator(gameRepository);

            var request = new GameRequestConcrete
            {
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.GameIdRequired, result.Error);
        }

        [Fact]
        public async Task ReturnFailureResultForGameNotFound()
        {
            var gameRepository = CreateGameRepositoryMock(
                gameExists: false);
            var validator = new GameRequestValidator(gameRepository);

            var request = new GameRequestConcrete
            {
                GameId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.GameNotFoundOrInactive, result.Error);
        }

        [Fact]
        public async Task ReturnFailureResultForGameInactive()
        {
            var gameRepository = CreateGameRepositoryMock(
                gameExists: true,
                gameIsActive: false);
            var validator = new GameRequestValidator(gameRepository);

            var request = new GameRequestConcrete
            {
                GameId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.GameNotFoundOrInactive, result.Error);
        }

        private IGamesRepository CreateGameRepositoryMock(bool gameExists = true, bool gameIsActive = true)
        {
            var mock = new Mock<IGamesRepository>();
            mock.Setup(m => m.GetGameById(It.IsAny<int>())).Returns(() =>
            {
                if (!gameExists)
                    return Task.FromResult<GameModel>(null);

                return Task.FromResult(new GameModel
                {
                    Status = gameIsActive ? Common.Enums.GameStatusEnum.InProgress : Common.Enums.GameStatusEnum.Cancelled,
                    IsActive = gameIsActive
                });
            });

            return mock.Object;
        }

        class GameRequestConcrete : IGameRequest
        {
            public int GameId { get; set; }
        }
    }
}

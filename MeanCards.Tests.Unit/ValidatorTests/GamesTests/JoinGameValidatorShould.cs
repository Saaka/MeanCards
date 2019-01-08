using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games;
using MeanCards.Model.DTO.Games;
using MeanCards.Model.DTO.Players;
using MeanCards.Tests.Unit.ValidatorTests.GamesTests.Mocks;
using MeanCards.Validators.Games;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Unit.ValidatorTests.GamesTests
{
    public class JoinGameValidatorShould
    {
        [Fact]
        public async Task ReturnSuccessForValidData()
        {
            var baseMock = BaseGameRequestsValidatorMock.CreateMock();
            var gameRepo = GamesRepositoryMock.Create().Object;
            var playersRepo = PlayersRepositoryMock.Create(
                playerExists: false).Object;
            var validator = new JoinGameValidator(baseMock.Object, gameRepo, playersRepo);

            var request = new JoinGame
            {
                GameId = 1,
                UserId = 1
            };

            var result = await validator.Validate(request);

            Assert.True(result.IsSuccessful);
            baseMock.Verify(x => x.Validate(request));
        }

        [Fact]
        public async Task ReturnFailureForUserThatAlreadyJoined()
        {
            var baseMock = BaseGameRequestsValidatorMock.CreateMock();
            var gameRepo = GamesRepositoryMock.Create().Object;
            var playersRepo = PlayersRepositoryMock.Create(
                playerExists: true).Object;
            var validator = new JoinGameValidator(baseMock.Object, gameRepo, playersRepo);

            var request = new JoinGame
            {
                GameId = 1,
                UserId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.UserAlreadyJoined, result.Error);
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
                    Status = gameIsActive ? Common.Enums.GameStatusEnum.InProgress : Common.Enums.GameStatusEnum.Canceled,
                    IsActive = gameIsActive
                });
            });

            return mock.Object;
        }
    }
}

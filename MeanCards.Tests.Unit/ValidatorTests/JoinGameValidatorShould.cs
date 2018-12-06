using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games;
using MeanCards.Validators.Games;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Unit.ValidatorTests
{
    public class JoinGameValidatorShould
    {
        [Fact]
        public async Task ReturnSuccessForValidData()
        {
            var gameRepo = CreateGameRepositoryMock();
            var usersRepo = CreateUserRepoMock();
            var validator = new JoinGameValidator(gameRepo, usersRepo);

            var request = new JoinGame
            {
                GameId = 1,
                UserId = 1
            };

            var result = await validator.Validate(request);

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public async Task ReturnFailureForMissingGameId()
        {
            var gameRepo = CreateGameRepositoryMock();
            var usersRepo = CreateUserRepoMock();
            var validator = new JoinGameValidator(gameRepo, usersRepo);

            var request = new JoinGame
            {
                UserId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.GameIdRequired, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForMissingUserId()
        {
            var gameRepo = CreateGameRepositoryMock();
            var usersRepo = CreateUserRepoMock();
            var validator = new JoinGameValidator(gameRepo, usersRepo);

            var request = new JoinGame
            {
                GameId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.UserIdRequired, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForNotExistingGame()
        {
            var gameRepo = CreateGameRepositoryMock(false);
            var usersRepo = CreateUserRepoMock();
            var validator = new JoinGameValidator(gameRepo, usersRepo);

            var request = new JoinGame
            {
                GameId = 1,
                UserId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.GameNotFoundOrInactive, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForNotExistingUser()
        {
            var gameRepo = CreateGameRepositoryMock();
            var usersRepo = CreateUserRepoMock(false);
            var validator = new JoinGameValidator(gameRepo, usersRepo);

            var request = new JoinGame
            {
                GameId = 1,
                UserId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Users.UserIdNotFound, result.Error);
        }

        private IGamesRepository CreateGameRepositoryMock(bool gameExists = true)
        {
            var mock = new Mock<IGamesRepository>();
            mock.Setup(m => m.ActiveGameExists(It.IsAny<int>())).Returns(Task.FromResult<bool>(gameExists));

            return mock.Object;
        }

        private IUsersRepository CreateUserRepoMock(bool userExists = true)
        {
            var mock = new Mock<IUsersRepository>();
            mock.Setup(m => m.ActiveUserExists(It.IsAny<int>())).Returns(Task.FromResult<bool>(userExists));

            return mock.Object;
        }
    }
}

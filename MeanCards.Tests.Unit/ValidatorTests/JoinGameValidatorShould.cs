using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games;
using MeanCards.Model.DTO.Games;
using MeanCards.Model.DTO.Players;
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
            var playersRepo = CreatePlayerRepo();
            var userRepo = CreateUserRepo();
            var validator = new JoinGameValidator(gameRepo, playersRepo, userRepo);

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
            var playersRepo = CreatePlayerRepo();
            var userRepo = CreateUserRepo();
            var validator = new JoinGameValidator(gameRepo, playersRepo, userRepo);

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
            var playersRepo = CreatePlayerRepo();
            var userRepo = CreateUserRepo();
            var validator = new JoinGameValidator(gameRepo, playersRepo, userRepo);

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
            var playersRepo = CreatePlayerRepo();
            var userRepo = CreateUserRepo();
            var validator = new JoinGameValidator(gameRepo, playersRepo, userRepo);

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
            var playersRepo = CreatePlayerRepo();
            var userRepo = CreateUserRepo(
                userExists: false);
            var validator = new JoinGameValidator(gameRepo, playersRepo, userRepo);

            var request = new JoinGame
            {
                GameId = 1,
                UserId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Users.UserIdNotFound, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForUserThatAlreadyJoined()
        {
            var gameRepo = CreateGameRepositoryMock(gameIsActive: true);
            var playersRepo = CreatePlayerRepo(
                userAlreadyJoined: true);
            var userRepo = CreateUserRepo();
            var validator = new JoinGameValidator(gameRepo, playersRepo, userRepo);

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
                    Status = gameIsActive ? Common.Enums.GameStatusEnum.InProgress : Common.Enums.GameStatusEnum.Canceled
                });
            });

            return mock.Object;
        }

        private IPlayersRepository CreatePlayerRepo(bool userAlreadyJoined = false)
        {
            var mock = new Mock<IPlayersRepository>();
            mock.Setup(m => m.GetPlayerByUserId(It.IsAny<int>(), It.IsAny<int>())).Returns(() =>
            {
                //if (!userExists)
                //    return Task.FromResult<PlayerModel>(null);

                return Task.FromResult(new PlayerModel { IsActive = userAlreadyJoined });
            });

            return mock.Object;
        }

        private IUsersRepository CreateUserRepo(bool userExists = true)
        {
            var mock = new Mock<IUsersRepository>();
            mock.Setup(m => m.ActiveUserExists(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<bool>(userExists);
            });

            return mock.Object;
        }
    }
}

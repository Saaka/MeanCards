﻿using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games;
using MeanCards.Validators.Games;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Unit.ValidatorTests
{
    public class StartGameRoundValidatorShould
    {
        [Fact]
        public async Task ReturnSuccessForValidData()
        {
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock();
            var gameRepo = CreateGameRepositoryMock();

            var validator = new StartGameRoundValidator(playersRepo, gameRoundRepo, gameRepo);

            var request = new StartGameRound
            {
                GameRoundId = 1,
                UserId = 1,
                GameId = 1
            };

            var result = await validator.Validate(request);

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public async Task ReturnSuccessForGameOwner()
        {
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock(
                isRoundOwner: false);
            var gameRepo = CreateGameRepositoryMock();

            var validator = new StartGameRoundValidator(playersRepo, gameRoundRepo, gameRepo);

            var request = new StartGameRound
            {
                GameRoundId = 1,
                UserId = 1,
                GameId = 1
            };

            var result = await validator.Validate(request);

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public async Task ReturnSuccessForGameRoundOwner()
        {
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock();
            var gameRepo = CreateGameRepositoryMock(
                isGameOwner: false);

            var validator = new StartGameRoundValidator(playersRepo, gameRoundRepo, gameRepo);

            var request = new StartGameRound
            {
                GameRoundId = 1,
                UserId = 1,
                GameId = 1
            };

            var result = await validator.Validate(request);

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public async Task ReturnFailureForMissingRoundId()
        {
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock();
            var gameRepo = CreateGameRepositoryMock();

            var validator = new StartGameRoundValidator(playersRepo, gameRoundRepo, gameRepo);

            var request = new StartGameRound
            {
                UserId = 1,
                GameId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.GameRoundIdRequired, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForMissingGameId()
        {
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock();
            var gameRepo = CreateGameRepositoryMock();

            var validator = new StartGameRoundValidator(playersRepo, gameRoundRepo, gameRepo);

            var request = new StartGameRound
            {
                UserId = 1,
                GameRoundId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.GameIdRequired, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForMissingUserId()
        {
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock();
            var gameRepo = CreateGameRepositoryMock();

            var validator = new StartGameRoundValidator(playersRepo, gameRoundRepo, gameRepo);

            var request = new StartGameRound
            {
                GameRoundId = 1,
                GameId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.UserIdRequired, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForInvalidUserAndPlayer()
        {
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock(
                isRoundOwner: false);
            var gameRepo = CreateGameRepositoryMock(
                isGameOwner: false);

            var validator = new StartGameRoundValidator(playersRepo, gameRoundRepo, gameRepo);

            var request = new StartGameRound
            {
                UserId = 1,
                GameRoundId = 1,
                GameId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.UserCantStartRound, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForInvalidUserPlayerCombination()
        {
            var playersRepo = CreatePlayersRepoMock(
                isUserLinkedWithPlayer: false);
            var gameRoundRepo = CreateGameRoundRepoMock();
            var gameRepo = CreateGameRepositoryMock();

            var validator = new StartGameRoundValidator(playersRepo, gameRoundRepo, gameRepo);

            var request = new StartGameRound
            {
                GameRoundId = 1,
                UserId = 1,
                GameId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Players.UserNotLinkedWithPlayer, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForInvalidRoundStatus()
        {
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock(
                isRoundPending: false);
            var gameRepo = CreateGameRepositoryMock();

            var validator = new StartGameRoundValidator(playersRepo, gameRoundRepo, gameRepo);

            var request = new StartGameRound
            {
                GameRoundId = 1,
                UserId = 1,
                GameId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.InvalidGameRoundStatus, result.Error);
        }

        [Fact]
        public async Task ReturnFailureForInvalidGameAndRoundCombination()
        {
            var playersRepo = CreatePlayersRepoMock();
            var gameRoundRepo = CreateGameRoundRepoMock(
                isRoundInGame: false);
            var gameRepo = CreateGameRepositoryMock();

            var validator = new StartGameRoundValidator(playersRepo, gameRoundRepo, gameRepo);

            var request = new StartGameRound
            {
                GameRoundId = 1,
                UserId = 1,
                GameId = 1
            };

            var result = await validator.Validate(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(ValidatorErrors.Games.RoundNotLinkedWithGame, result.Error);
        }

        private IGameRoundsRepository CreateGameRoundRepoMock(
            bool isRoundOwner = true, 
            bool isRoundPending = true,
            bool isRoundInGame = true)
        {
            var mock = new Mock<IGameRoundsRepository>();

            return mock.Object;
        }

        private IPlayersRepository CreatePlayersRepoMock(bool isUserLinkedWithPlayer = true)
        {
            var mock = new Mock<IPlayersRepository>();

            return mock.Object;
        }

        private IGamesRepository CreateGameRepositoryMock(bool isGameOwner = true)
        {
            var mock = new Mock<IGamesRepository>();

            return mock.Object;
        }
    }
}

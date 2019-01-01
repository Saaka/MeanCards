﻿using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games;
using MeanCards.Model.DTO.Games;
using MeanCards.Model.DTO.Players;
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
            var gameRepo = CreateGameRepositoryMock();
            var playersRepo = CreatePlayerRepo();
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
            var gameRepo = CreateGameRepositoryMock(gameIsActive: true);
            var playersRepo = CreatePlayerRepo(
                userAlreadyJoined: true);
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
                return Task.FromResult(new PlayerModel { IsActive = userAlreadyJoined });
            });

            return mock.Object;
        }
    }
}
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.DTO.Games;
using Moq;
using System.Threading.Tasks;

namespace MeanCards.Tests.Unit.ValidatorTests.GamesTests.Mocks
{
    public static class GamesRepositoryMock
    {
        public static Mock<IGamesRepository> Create(
            bool gameExists = true,
            bool isGameOwner = true,
            bool gameIsActive = true,
            Common.Enums.GameStatusEnum gameStatus = Common.Enums.GameStatusEnum.InProgress)
        {
            var mock = new Mock<IGamesRepository>();
            mock.Setup(m => m.GetGameById(It.IsAny<int>())).Returns(() =>
            {
                if (!gameExists)
                    return Task.FromResult<GameModel>(null);

                return Task.FromResult(new GameModel
                {
                    OwnerId = isGameOwner ? MockConstants.GameOwnerId : int.MaxValue,
                    Status = gameIsActive ? Common.Enums.GameStatusEnum.InProgress : Common.Enums.GameStatusEnum.Cancelled,
                    IsActive = gameIsActive
                });
            });
            mock.Setup(m => m.GetGameStatus(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult(gameStatus);
            });

            return mock;
        }
    }
}

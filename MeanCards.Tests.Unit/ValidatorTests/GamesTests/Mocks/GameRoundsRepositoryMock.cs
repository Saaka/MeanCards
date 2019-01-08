using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.DTO.Games;
using Moq;
using System.Threading.Tasks;

namespace MeanCards.Tests.Unit.ValidatorTests.GamesTests.Mocks
{
    public class GameRoundsRepositoryMock
    {
        public static Mock<IGameRoundsRepository> Create(
            bool isRoundOwner = true,
            Common.Enums.GameRoundStatusEnum status = Common.Enums.GameRoundStatusEnum.Finished,
            bool isRoundInGame = true)
        {
            var mock = new Mock<IGameRoundsRepository>();
            mock.Setup(m => m.GetGameRound(It.IsAny<int>(), It.IsAny<int>())).Returns(() =>
            {
                if (!isRoundInGame)
                    return Task.FromResult<GameRoundModel>(null);

                return Task.FromResult(new GameRoundModel
                {
                    Status = status,
                    OwnerPlayerId = isRoundOwner ? MockConstants.RoundOwnerId : int.MaxValue,
                    GameRoundId = MockConstants.RoundId
                });
            });

            return mock;
        }
    }
}

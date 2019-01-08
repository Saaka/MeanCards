using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.DTO.Players;
using Moq;
using System.Threading.Tasks;

namespace MeanCards.Tests.Unit.ValidatorTests.GamesTests.Mocks
{
    public class PlayersRepositoryMock
    {
        public static Mock<IPlayersRepository> Create(
            bool isUserLinkedWithPlayer = true,
            bool playerExists = true,
            bool playerIsActive = true,
            bool roundHasEnoughPlayers = true)
        {
            var mock = new Mock<IPlayersRepository>();
            mock.Setup(m => m.GetPlayerByUserId(It.IsAny<int>(), It.IsAny<int>())).Returns(() =>
            {
                if (!isUserLinkedWithPlayer || ! playerExists)
                    return Task.FromResult<PlayerModel>(null);

                return Task.FromResult(new PlayerModel
                {
                    PlayerId = MockConstants.CardOwnerId,
                    IsActive = playerIsActive
                });
            });

            mock.Setup(m => m.GetActivePlayersCount(It.IsAny<int>())).Returns(() =>
            {
                if (!roundHasEnoughPlayers)
                    return Task.FromResult<int>(1);

                return Task.FromResult<int>(GameConstants.MinimumPlayersCount);
            });

            return mock;
        }
    }
}

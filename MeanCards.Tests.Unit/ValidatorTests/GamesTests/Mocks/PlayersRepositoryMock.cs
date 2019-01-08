using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.DTO.Players;
using Moq;
using System.Threading.Tasks;

namespace MeanCards.Tests.Unit.ValidatorTests.GamesTests.Mocks
{
    public class PlayersRepositoryMock
    {
        public static Mock<IPlayersRepository> Create(bool isUserLinkedWithPlayer = true)
        {
            var mock = new Mock<IPlayersRepository>();
            mock.Setup(m => m.GetPlayerByUserId(It.IsAny<int>(), It.IsAny<int>())).Returns(() =>
            {
                if (!isUserLinkedWithPlayer)
                    return Task.FromResult<PlayerModel>(null);

                return Task.FromResult(new PlayerModel
                {
                    PlayerId = MockConstants.CardOwnerId
                });
            });

            return mock;
        }
    }
}

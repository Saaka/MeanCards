using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using Moq;
using System.Threading.Tasks;

namespace MeanCards.Tests.Unit.ValidatorTests.GamesTests.Mocks
{
    public static class PlayerAnswersRepositoryMock
    {
        public static Mock<IPlayerAnswersRepository> Create(bool hasEnoughAnswers = true)
        {
            var mock = new Mock<IPlayerAnswersRepository>();
            mock.Setup(m => m.GetNumberOfAnswers(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<int>(hasEnoughAnswers ? GameConstants.MinimumAnswersCount : 0);
            });

            return mock;
        }
    }
}

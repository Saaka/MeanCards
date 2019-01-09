using MeanCards.Common.Constants;
using MeanCards.Model.Core.Games.Base;
using MeanCards.Validators;
using MeanCards.Validators.Games.ValidationRules;
using Moq;
using System.Threading.Tasks;

namespace MeanCards.Tests.Unit.ValidatorTests.GamesTests.Mocks
{
    public class GameOrRoundOwnerRuleMock
    {
        public static Mock<IGameOrRoundOwnerRule> Create<T>(bool isOwner = true)
            where T: IGameRoundRequest, IPlayerRequest
        {
            var mock = new Mock<IGameOrRoundOwnerRule>();
            mock.Setup(m => m.Validate(It.IsAny<T>())).Returns(() =>
            {
                if (isOwner)
                    return Task.FromResult(new ValidatorResult());
                else
                    return Task.FromResult(new ValidatorResult(ValidatorErrors.Games.InvalidUserAction));
            })
            .Verifiable();

            return mock;
        }
    }
}

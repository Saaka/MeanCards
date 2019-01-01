using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core;
using MeanCards.Validators.Games.Base;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Unit.ValidatorTests.GamesTests.BaseTests
{
    public class UserRequestValidatorShould
    {
        [Fact]
        public async Task ReturnSuccessResultForValidData()
        {
            var playerRepo = CreateUsersRepoMock();
            var validator = new UserRequestValidator(playerRepo);

            var request = new UserRequestConcrete
            {
                UserId = 1
            };

            var result = await validator.Validate(request);

            Assert.True(result.IsSuccessful);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task ReturnFailureResultForMissingUserId()
        {
            {
                var playerRepo = CreateUsersRepoMock();
                var validator = new UserRequestValidator(playerRepo);

                var request = new UserRequestConcrete
                {
                };

                var result = await validator.Validate(request);

                Assert.False(result.IsSuccessful);
                Assert.Equal(ValidatorErrors.Games.UserIdRequired, result.Error);
            }
        }

        [Fact]
        public async Task ReturnFailureResultForMissingOrInactiveUser()
        {
            {
                var playerRepo = CreateUsersRepoMock(
                    userExists: false);
                var validator = new UserRequestValidator(playerRepo);

                var request = new UserRequestConcrete
                {
                    UserId = 1
                };

                var result = await validator.Validate(request);

                Assert.False(result.IsSuccessful);
                Assert.Equal(ValidatorErrors.Users.UserIdNotFound, result.Error);
            }
        }

        private IUsersRepository CreateUsersRepoMock(bool userExists = true)
        {
            var mock = new Mock<IUsersRepository>();
            mock.Setup(m => m.ActiveUserExists(It.IsAny<int>())).Returns(Task.FromResult<bool>(userExists));

            return mock.Object;
        }

        class UserRequestConcrete : IUserRequest
        {
            public int UserId { get; set; }
        }
    }
}

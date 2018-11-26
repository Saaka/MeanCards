using MeanCards.Common.Constants;
using MeanCards.Model.Core.Users;
using MeanCards.Tests.Integration.BaseTests;
using MeanCards.UserManagement;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.UserManagementTests
{
    public class GetUserByCredentialsHandlerShould : BaseDomainTests
    {
        [Fact]
        public async Task GetExistingUserData()
        {
            await CreateUser("test@test.com", "pass12");

            var handler = Fixture.GetService<IGetUserByCredentialsHandler>();

            var request = new GetUserByCredentials
            {
                Email = "test@test.com",
                Password = "pass12"
            };

            var result = await handler.Handle(request);

            Assert.True(result.IsSuccessful);
            Assert.Null(result.Error);
            Assert.NotNull(result.User);
            Assert.Equal("test@test.com", result.User.Email);
        }

        [Fact]
        public async Task NotGetUserForNotExistingUser()
        {
            await CreateUser("test@test.com", "pass12");

            var handler = Fixture.GetService<IGetUserByCredentialsHandler>();

            var request = new GetUserByCredentials
            {
                Email = "test1@test.com",
                Password = "pass123"
            };

            var result = await handler.Handle(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(AccessErrors.UserNotFound, result.Error);
            Assert.Null(result.User);
        }

        [Fact]
        public async Task NotGetUserForInvalidCredentials()
        {
            await CreateUser("test@test.com", "pass12");

            var handler = Fixture.GetService<IGetUserByCredentialsHandler>();

            var request = new GetUserByCredentials
            {
                Email = "test@test.com",
                Password = "pass123"
            };

            var result = await handler.Handle(request);

            Assert.False(result.IsSuccessful);
            Assert.Equal(AccessErrors.InvalidUserCredentials, result.Error);
            Assert.Null(result.User);
        }

        private async Task CreateUser(string email, string password)
        {
            var handler = Fixture.GetService<ICreateUserHandler>();

            var request = new CreateUser
            {
                Email = email,
                Password = password,
                DisplayName = "Jimmy"
            };

            await handler.Handle(request);
        }
    }
}

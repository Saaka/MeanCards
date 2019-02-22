using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Users;
using MeanCards.Tests.Integration.BaseTests;
using MeanCards.UserManagement;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.UserManagementTests
{
    public class CreateUserHandlerShould : BaseDomainTests
    {
        [Fact]
        public async Task CreateUserForValidRequest()
        {
            var handler = Fixture.GetService<ICreateUserHandler>();

            var request = new CreateUser
            {
                Email = "test@test.com",
                Password = "pass12",
                DisplayName = "Jimmy"
            };

            var result = await handler.Handle(request, new System.Threading.CancellationToken());

            Assert.NotNull(result);
            Assert.True(result.IsSuccessful);
            Assert.Null(result.Error);
            Assert.NotNull(result.User);
            Assert.Equal(request.Email, result.User.Email);
            Assert.Equal(request.DisplayName, result.User.DisplayName);
            Assert.Equal(request.DisplayName, result.User.UserName);
            Assert.NotEmpty(result.User.Code);
            Assert.NotEmpty(result.User.ImageUrl);
            Assert.NotEqual(0, result.User.UserId);
        }

        [Fact]
        public async Task NotCreateUserForInvalidRequest()
        {
            var handler = Fixture.GetService<ICreateUserHandler>();

            var request = new CreateUser
            {
                DisplayName = "Jimmy"
            };

            var result = await handler.Handle(request, new System.Threading.CancellationToken());

            Assert.NotNull(result);
            Assert.False(result.IsSuccessful);
            Assert.NotEmpty(result.Error);
        }
    }
}

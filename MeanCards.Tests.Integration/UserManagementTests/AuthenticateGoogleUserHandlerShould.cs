using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Users;
using MeanCards.Tests.Integration.BaseTests;
using MeanCards.UserManagement;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.UserManagementTests
{
    public class AuthenticateGoogleUserHandlerShould : BaseDomainTests
    {
        [Fact]
        public async Task CreateUserForNewEmail()
        {
            var handler = Fixture.GetService<IAuthenticateGoogleUserHandler>();

            var request = new AuthenticateGoogleUser
            {
                GoogleId = Guid.NewGuid().ToString(),
                DisplayName = "Test",
                Email = "test@test.com",
                ImageUrl = "https://picsum.photos/96/96/?random"
            };

            var result = await handler.Handle(request, new System.Threading.CancellationToken());

            Assert.NotNull(result);
            Assert.True(result.IsSuccessful);
            Assert.NotNull(result.User);
        }

        [Fact]
        public async Task MergeExistingUserWithGoogle()
        {
            var repository = Fixture.GetService<IUsersRepository>();
            var passwordHandler = Fixture.GetService<ICreateUserHandler>();
            var googleHandler = Fixture.GetService<IAuthenticateGoogleUserHandler>();

            var newResult = passwordHandler.Handle(new CreateUser
            {
                DisplayName = "Test1",
                Email = "test@test.com",
                Password = "pass12"
            }, new System.Threading.CancellationToken());

            var request = new AuthenticateGoogleUser
            {
                GoogleId = Guid.NewGuid().ToString(),
                DisplayName = "Test",
                Email = "test@test.com",
                ImageUrl = "https://picsum.photos/96/96/?random"
            };

            var result = await googleHandler.Handle(request, new System.Threading.CancellationToken());
            var googleUserExists = await repository.GoogleUserExists("test@test.com", request.GoogleId);

            Assert.NotNull(result);
            Assert.True(result.IsSuccessful);
            Assert.NotNull(result.User);
            Assert.True(googleUserExists);
        }

        [Fact]
        public async Task CreateUpdateExistingGoogleUser()
        {
            var handler = Fixture.GetService<IAuthenticateGoogleUserHandler>();

            var googleId = Guid.NewGuid().ToString();
            var firstRequest = new AuthenticateGoogleUser
            {
                GoogleId = googleId,
                DisplayName = "Test",
                Email = "test@test.com",
                ImageUrl = "https://picsum.photos/96/96/?image=22"
            };

            var newResult = await handler.Handle(firstRequest, new System.Threading.CancellationToken());

            var secondRequest = new AuthenticateGoogleUser
            {
                GoogleId = googleId,
                DisplayName = "Test",
                Email = "test@test.com",
                ImageUrl = "https://picsum.photos/96/96/?image=111"
            };

            var existingResult = await handler.Handle(secondRequest, new System.Threading.CancellationToken());

            Assert.NotNull(existingResult);
            Assert.True(existingResult.IsSuccessful);
            Assert.NotNull(existingResult.User);

            Assert.Equal(newResult.User.UserId, existingResult.User.UserId);
            Assert.Equal(newResult.User.Code, existingResult.User.Code);
            Assert.Equal("Test", existingResult.User.DisplayName);
            Assert.Equal("test@test.com", existingResult.User.UserName);
            Assert.Equal("test@test.com", existingResult.User.Email);
            Assert.Equal("https://picsum.photos/96/96/?image=111", existingResult.User.ImageUrl);
        }

        [Fact]
        public async Task FailForMissingGoogleId()
        {
            var handler = Fixture.GetService<IAuthenticateGoogleUserHandler>();

            var request = new AuthenticateGoogleUser
            {
                DisplayName = "Test",
                Email = "test@test.com",
                ImageUrl = "https://picsum.photos/96/96/?random"
            };

            var result = await handler.Handle(request, new System.Threading.CancellationToken());

            Assert.NotNull(result);
            Assert.False(result.IsSuccessful);
            Assert.NotEmpty(result.Error);
            Assert.Null(result.User);
        }

        [Fact]
        public async Task FailForMissingEmail()
        {
            var handler = Fixture.GetService<IAuthenticateGoogleUserHandler>();

            var request = new AuthenticateGoogleUser
            {
                GoogleId = Guid.NewGuid().ToString(),
                DisplayName = "Test",
                ImageUrl = "https://picsum.photos/96/96/?random"
            };

            var result = await handler.Handle(request, new System.Threading.CancellationToken());

            Assert.NotNull(result);
            Assert.False(result.IsSuccessful);
            Assert.NotEmpty(result.Error);
            Assert.Null(result.User);
        }

        [Fact]
        public async Task FailForMissingName()
        {
            var handler = Fixture.GetService<IAuthenticateGoogleUserHandler>();

            var request = new AuthenticateGoogleUser
            {
                GoogleId = Guid.NewGuid().ToString(),
                Email = "test@test.com",
                ImageUrl = "https://picsum.photos/96/96/?random"
            };

            var result = await handler.Handle(request, new System.Threading.CancellationToken());

            Assert.NotNull(result);
            Assert.False(result.IsSuccessful);
            Assert.NotEmpty(result.Error);
            Assert.Null(result.User);
        }
    }
}

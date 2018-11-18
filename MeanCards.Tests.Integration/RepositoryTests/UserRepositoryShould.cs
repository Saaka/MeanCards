using MeanCards.DAL.Interfaces.Repository;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.RepositoryTests
{
    public class UserRepositoryShould : BaseRepositoryTests
    {
        [Fact]
        public async Task CreateUserAuth()
        {
            var userRepository = Fixture.GetService<IUsersRepository>();
            var createModel = new Model.Creation.Users.CreateUserModel { DisplayName = "TestName", Password = "TestPassword", Email = "test@test.com", ImageUrl = "http://test.com" };

            await userRepository.CreateUser(createModel);

            var users = await userRepository.GetAllActiveUsers();

            Assert.Single(users);

            var user = users.First();
            Assert.Equal("TestName", user.DisplayName);
            Assert.Equal("test@test.com", user.Email);
            Assert.Equal("http://test.com", user.ImageUrl);
            Assert.True(user.IsActive);

            var userAuth = await userRepository.GetUserAuth(createModel.Email);
            Assert.Equal(user.UserId, userAuth.UserId);
            Assert.Equal("TestName", userAuth.DisplayName);
            Assert.Equal("http://test.com", userAuth.ImageUrl);
            Assert.Equal("TestPassword", userAuth.Password);
        }

        [Fact]
        public async Task CreateUserGoogleAuth()
        {
            var userRepository = Fixture.GetService<IUsersRepository>();
            var createModel = new Model.Creation.Users.CreateGoogleUserModel { DisplayName = "TestName", GoogleId = "TestGoogleId", Email = "test@test.com", ImageUrl = "http://test.com"};

            await userRepository.CreateUser(createModel);

            var users = await userRepository.GetAllActiveUsers();

            Assert.Single(users);

            var user = users.First();
            Assert.Equal("TestName", user.DisplayName);
            Assert.Equal("test@test.com", user.Email);
            Assert.Equal("http://test.com", user.ImageUrl);
            Assert.True(user.IsActive);

            var userAuth = await userRepository.GetGoogleUser(createModel.GoogleId);
            Assert.Equal(user.UserId, userAuth.UserId);
            Assert.Equal("TestName", userAuth.DisplayName);
            Assert.Equal("http://test.com", userAuth.ImageUrl);
            Assert.Equal("TestGoogleId", userAuth.GoogleId);
        }
    }
}

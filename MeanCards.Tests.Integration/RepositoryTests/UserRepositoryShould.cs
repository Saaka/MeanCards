using MeanCards.DAL.Interfaces.Repository;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.RepositoryTests
{
    public class UserRepositoryShould : BaseRepositoryTests
    {
        [Fact]
        public async Task CreateUser()
        {
            var userRepository = Fixture.GetService<IUsersRepository>();
            var createModel = new Model.Creation.CreateUserModel { DisplayName = "TestName" };

            await userRepository.CreateUser(createModel);

            var users = await userRepository.GetAllActiveUsers();

            Assert.Single(users);

            var user = users.First();
            Assert.Equal("TestName", user.DisplayName);
            Assert.True(user.IsActive);
        }
    }
}

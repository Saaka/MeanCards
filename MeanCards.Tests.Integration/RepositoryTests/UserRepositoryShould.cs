using MeanCards.DAL.Repository;
using MeanCards.DAL.Storage;
using MeanCards.Tests.Integration.Helpers;
using System.Threading.Tasks;
using Xunit;

namespace MeanCards.Tests.Integration.RepositoryTests
{
    public class UserRepositoryShould
    {
        [Fact]
        public async Task CreateUserForValidData()
        {
            var options = TestInMemoryDbOptionsProvider.CreateOptions<AppDbContext>();
            var createModel = new Model.Creation.CreateUserModel { DisplayName = "TestName" };

            using (var context = new AppDbContext(options))
            {
                var repository = new UserRepository(context);
                await repository.CreateUser(createModel);
            }

            using (var context = new AppDbContext(options))
            {
                var repository = new UserRepository(context);
                var users = await repository.GetAllActiveUsers();

                Assert.Single(users);
            }
        }
    }
}

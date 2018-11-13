using MeanCards.DAL;
using MeanCards.DAL.Interfaces.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace MeanCards.Tests.Integration.Config
{
    public class DALServiceCollectionFixture : ServiceCollectionFixture
    {
        public const string DefaultLanguageCode = "PL";
        public const string DefaultLanguageName = "Polski";
        public const string DefaultUserName = "Kowalski";

        public override IServiceCollection RegisterServices(IServiceCollection serviceCollection)
        {
            return serviceCollection
                .RegisterInmemoryContext()
                .RegisterDAL();
        }

        public async Task<int> CreateDefaultUser()
        {
            var usersRepository = GetService<IUsersRepository>();
            return await usersRepository.CreateUser(new Model.Creation.CreateUserModel { DisplayName = DefaultUserName });
        }

        public async Task<int> CreateDefaultLanguage()
        {
            var languageRepository = GetService<ILanguagesRepository>();
            return await languageRepository.CreateLanguage(new Model.Creation.CreateLanguageModel { Code = DefaultLanguageCode, Name = DefaultLanguageName });
        }
    }
}

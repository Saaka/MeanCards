using MeanCards.Common;
using MeanCards.DAL;
using MeanCards.DAL.Interfaces.Initializer;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Storage;
using MeanCards.Model.DAL.Creation.AnswerCards;
using MeanCards.Model.DAL.Creation.Languages;
using MeanCards.Model.DAL.Creation.QuestionCards;
using MeanCards.Model.DAL.Creation.Users;
using MeanCards.Tests.Base.Fixtures;
using MeanCards.Tests.Core.Config;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeanCards.Tests.Core
{
    public class CoreServiceCollectionFixture : ServiceCollectionFixture
    {
        public override IServiceCollection RegisterServices(IServiceCollection serviceCollection)
        {
            var config = CoreTestsConfiguration.InitConfiguration();
            var databaseName = $"MC_TESTS_{Guid.NewGuid().ToString("N")}";
            return serviceCollection
                .RegisterCoreTestsContext(config, databaseName)
                .RegisterDAL()
                .RegisterIdentityStore()
                .RegisterDomainServices()
                .RegisterCommon();
        }

        protected override void OnServiceCollectionInitialized()
        {
            var initializer = GetService<IDbInitializer>();

            initializer.Execute().Wait();
        }

        public async Task<int> CreateDefaultUser(
            string userName = "Kowalski",
            string email = "test @test.com",
            string password = "TestPassword1!",
            string userCode = "12345")
        {
            var usersRepository = GetService<IUsersRepository>();

            var result = await usersRepository.CreateUser(new CreateUserModel { DisplayName = userName, Email = email, Password = password, Code = userCode });
            return result.Model.UserId;
        }

        public async Task<int> CreateDefaultLanguage(
            string code = "PL",
            string name = "Polski")
        {
            var languageRepository = GetService<ILanguagesRepository>();
            return await languageRepository.CreateLanguage(new CreateLanguageModel { Code = code, Name = name });
        }

        public async Task CreateQuestionCards(
            int languageId,
            int cardCount = 1,
            bool includeAdultContent = false)
        {
            var repository = GetService<IQuestionCardsRepository>();

            var cardsToCreate = new List<CreateQuestionCardModel>();
            for (int i = 0; i < cardCount; i++)
            {
                cardsToCreate.Add(new CreateQuestionCardModel
                {
                    LanguageId = languageId,
                    NumberOfAnswers = 1,
                    Text = $"QC Test{i}",
                    IsAdultContent = includeAdultContent ? i % 2 == 0 : false
                });
            }
            await repository.CreateQuestionCards(cardsToCreate);
        }

        public async Task CreateAnswerCards(
            int languageId,
            int cardsCount = 20,
            bool includeAdultContent = false)
        {
            var cards = new List<CreateAnswerCardModel>();
            for (int i = 0; i < cardsCount; i++)
            {
                cards.Add(new CreateAnswerCardModel
                {
                    IsAdultContent = includeAdultContent ? i % 2 == 0 : false,
                    LanguageId = languageId,
                    Text = $"AC Test{i}"
                });
            }
            var cardRepository = GetService<IAnswerCardsRepository>();

            await cardRepository.CreateAnswerCards(cards);
        }

        private bool isDisposed = false;
        public override void Dispose()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                var context = GetService<AppDbContext>();
                context.Database.EnsureDeleted();
            }
        }
    }
}

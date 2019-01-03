using MeanCards.Common;
using MeanCards.DAL;
using MeanCards.DAL.Interfaces.Initializer;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Storage;
using MeanCards.GameManagement;
using MeanCards.Model.Core.Games;
using MeanCards.Model.DAL.Creation.AnswerCards;
using MeanCards.Model.DAL.Creation.Languages;
using MeanCards.Model.DAL.Creation.QuestionCards;
using MeanCards.Model.DAL.Creation.Users;
using MeanCards.Model.DTO.Games;
using MeanCards.Tests.Base.Fixtures;
using MeanCards.Tests.Core.Config;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeanCards.Tests.Core
{
    public class CoreServiceCollectionFixture : ServiceCollectionFixture
    {
        public override IServiceCollection RegisterServices(IServiceCollection serviceCollection)
        {
            var config = CoreTestsConfiguration.InitConfiguration();
            var databaseName = $"MC_TESTS_{DateTime.Now.ToString("yyyyMMddHHmmss")}_{Guid.NewGuid().ToString("N")}";
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

        public async Task<GameModel> CreateGame(
            string gameName = "TestGame",
            bool showAdultContent = false,
            int questionCardsAvailable = 20,
            int answerCardsAvailable = 100,
            int pointsLimit = 10,
            int additionalPlayersCount = 2)
        {
            var userId = await CreateDefaultUser();
            var languageId = await CreateDefaultLanguage();
            await CreateQuestionCards(
                languageId: languageId,
                cardCount: questionCardsAvailable);
            await CreateAnswerCards(
                languageId: languageId,
                cardsCount: answerCardsAvailable);

            var handler = GetService<ICreateGameHandler>();

            var result = await handler.Handle(new CreateGame
            {
                LanguageId = languageId,
                Name = gameName,
                UserId = userId,
                ShowAdultContent = showAdultContent,
                PointsLimit = pointsLimit
            });

            await AddPlayersToGame(result.GameId, additionalPlayersCount);

            var gamesRepository = GetService<IGamesRepository>();

            return await gamesRepository.GetGameById(result.GameId);
        }

        private async Task AddPlayersToGame(int gameId, int playersCount)
        {
            int players = 0;
            while(players < playersCount)
            {
                await AddNewPlayerToGame(gameId);
                players++;
            }
        }

        public async Task StartGameRound(int gameId, int gameRoundId, int userId)
        {
            var handler = GetService<IStartGameRoundHandler>();
            await handler.Handle(new StartGameRound
            {
                GameRoundId = gameRoundId,
                GameId = gameId,
                UserId = userId
            });
        }

        public async Task AddNewPlayerToGame(int gameId)
        {
            var userId = await CreateDefaultUser(
                userName: Guid.NewGuid().ToString("N"),
                email: $"{Guid.NewGuid().ToString()}@test.com",
                userCode: Guid.NewGuid().ToString("N"));
            await JoinPlayer(gameId, userId);
        }

        public async Task<JoinGameResult> JoinPlayer(int gameId, int userId)
        {
            var handler = GetService<IJoinGameHandler>();
            return await handler.Handle(new JoinGame
            {
                GameId = gameId,
                UserId = userId
            });
        }

        public async Task<int> GetRandomPlayerCard(int playerId)
        {
            var repo = GetService<IPlayerCardsRepository>();

            var cards = await repo.GetUnusedPlayerCards(playerId);

            return cards.Select(x=> x.PlayerCardId).FirstOrDefault();
        }

        public async Task<GameRoundModel> GetCurrentGameRound(int gameId)
        {
            var gameRoundRepository = GetService<IGameRoundsRepository>();
            return await gameRoundRepository.GetCurrentGameRound(gameId);
        }

        public async Task<int> CreateDefaultUser(
            string userName = "Kowalski",
            string email = "test@test.com",
            string password = "TestPassword1!",
            string userCode = "12345")
        {
            var usersRepository = GetService<IUsersRepository>();

            var result = await usersRepository.CreateUser(new CreateUserModel { DisplayName = userName, Email = email, Password = password, Code = userCode });
            return result.Model.UserId;
        }

        public async Task<int> CreateRandomUser()
        {
            var uuid = Guid.NewGuid().ToString("N");
            return await CreateDefaultUser(uuid, $"{uuid}@test.com", userCode: uuid);
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

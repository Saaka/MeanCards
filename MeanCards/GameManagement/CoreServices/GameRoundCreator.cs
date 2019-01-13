using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.DAL.Creation.Games;
using System.Threading.Tasks;

namespace MeanCards.GameManagement.CoreServices
{
    public interface IGameRoundCreator
    {
        Task<CreateRoundResult> CreateFirstRound(int gameId, int ownerId);
        Task<CreateRoundResult> CreateRound(int gameId, int gameRoundId);
    }

    public class GameRoundCreator : IGameRoundCreator
    {
        private readonly INextGameRoundOwnerProvider nextGameRoundOwnerProvider;
        private readonly IQuestionCardsRepository questionCardsRepository;
        private readonly IGameRoundsRepository gameRoundsRepository;

        public GameRoundCreator(
            INextGameRoundOwnerProvider nextGameRoundOwnerProvider,
            IQuestionCardsRepository questionCardsRepository,
            IGameRoundsRepository gameRoundsRepository)
        {
            this.nextGameRoundOwnerProvider = nextGameRoundOwnerProvider;
            this.questionCardsRepository = questionCardsRepository;
            this.gameRoundsRepository = gameRoundsRepository;
        }

        public async Task<CreateRoundResult> CreateFirstRound(int gameId, int ownerId)
        {
            var questionCard = await questionCardsRepository
                .GetRandomQuestionCardForGame(gameId);
            if (questionCard == null)
                return new CreateRoundResult(GameErrors.NoQuestionCardsAvailable);

            return await CreateRound(gameId, ownerId, questionCard.QuestionCardId, 1);
        }

        public async Task<CreateRoundResult> CreateRound(int gameId, int gameRoundId)
        {
            var questionCard = await questionCardsRepository
                .GetRandomQuestionCardForGame(gameId);
            if (questionCard == null)
                return new CreateRoundResult(GameErrors.NoQuestionCardsAvailable);

            var prevRound = await gameRoundsRepository
                .GetGameRound(gameId, gameRoundId);

            var nextOwnerResult = await nextGameRoundOwnerProvider.GetNextOwner(gameId, prevRound.OwnerPlayerId);
            if (!nextOwnerResult.IsSuccessful)
                return new CreateRoundResult(GameErrors.CouldNotFindNextRoundOwner);

            return await CreateRound(gameId, nextOwnerResult.PlayerId, questionCard.QuestionCardId, ++prevRound.Number);
        }

        private async Task<CreateRoundResult> CreateRound(int gameId, int ownerId, int questionCardId, int roundNumber)
        {
            var gameRound = await gameRoundsRepository.CreateGameRound(new CreateGameRoundModel
            {
                GameId = gameId,
                OwnerPlayerId = ownerId,
                QuestionCardId = questionCardId,
                RoundNumber = roundNumber
            });

            return new CreateRoundResult(gameRound.GameRoundId, ownerId);
        }
    }

    public class CreateRoundResult
    {
        public CreateRoundResult(string error)
        {
            IsSuccessful = false;
            Error = error;
        }
        public CreateRoundResult(int roundId, int ownerPlayerId)
        {
            IsSuccessful = true;
            RoundId = roundId;
            OwnerPlayerId = ownerPlayerId;
        }

        public bool IsSuccessful { get; set; }
        public string Error { get; set; }
        public int RoundId { get; set; }
        public int OwnerPlayerId { get; set; }
    }
}

using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.DAL.Creation.Players;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeanCards.GameManagement.CoreServices
{
    public interface IPlayerCardsCreator
    {
        Task<CreatePlayerCardsResult> CreateCards(int gameId);
        Task<CreatePlayerCardsResult> CreateCardsForPlayer(int gameId, int playerId);
    }
    public class PlayerCardsCreator : IPlayerCardsCreator
    {
        private readonly IQuestionCardsRepository questionCardsRepository;
        private readonly IPlayerCardsRepository playerCardsRepository;
        private readonly IAnswerCardsRepository answerCardsRepository;

        public PlayerCardsCreator(
            IQuestionCardsRepository questionCardsRepository,
            IPlayerCardsRepository playerCardsRepository,
            IAnswerCardsRepository answerCardsRepository)
        {
            this.questionCardsRepository = questionCardsRepository;
            this.playerCardsRepository = playerCardsRepository;
            this.answerCardsRepository = answerCardsRepository;
        }

        public async Task<CreatePlayerCardsResult> CreateCards(int gameId)
        {
            var players = await playerCardsRepository.GetPlayersCardsInfo(gameId);
            var cardsToAdd = new List<CreatePlayerCardModel>();

            foreach (var player in players)
            {
                var missingCardCount = GameConstants.StartingPlayerCardsCount - player.PlayerCardsCount;

                if (missingCardCount > 0)
                {
                    var toAdd = await CreatePlayerAnswerCards(gameId, player.PlayerId, missingCardCount);
                    if(!toAdd.Any() || toAdd.Count < missingCardCount)
                        return new CreatePlayerCardsResult(GameErrors.NotEnoughAnswerCards);

                    cardsToAdd.AddRange(toAdd);
                }
            }
            if (cardsToAdd.Any())
                await playerCardsRepository.CreatePlayerCards(cardsToAdd);

            return new CreatePlayerCardsResult();
        }

        public async Task<CreatePlayerCardsResult> CreateCardsForPlayer(int gameId, int playerId)
        {
            var cardsToAdd = await CreatePlayerAnswerCards(gameId, playerId, GameConstants.StartingPlayerCardsCount);
            if (!cardsToAdd.Any() || cardsToAdd.Count < GameConstants.StartingPlayerCardsCount)
                return new CreatePlayerCardsResult(GameErrors.NotEnoughAnswerCards);

            await playerCardsRepository.CreatePlayerCards(cardsToAdd);

            return new CreatePlayerCardsResult();
        }

        private async Task<List<CreatePlayerCardModel>> CreatePlayerAnswerCards(int gameId, int playerId, int count)
        {
            var cards = await answerCardsRepository.GetRandomAnswerCardsForGame(gameId, count);

            return cards.Select(c => new CreatePlayerCardModel
            {
                PlayerId = playerId,
                AnswerCardId = c.AnswerCardId
            }).ToList();
        }
    }

    public class CreatePlayerCardsResult
    {
        public CreatePlayerCardsResult(string error)
        {
            IsSuccessful = false;
            Error = error;
        }
        public CreatePlayerCardsResult()
        {
            IsSuccessful = true;
        }

        public bool IsSuccessful { get; set; }
        public string Error { get; set; }
    }
}

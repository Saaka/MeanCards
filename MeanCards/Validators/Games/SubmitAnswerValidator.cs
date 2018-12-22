using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games
{
    public class SubmitAnswerValidator : IRequestValidator<SubmitAnswer>
    {
        private readonly IPlayersRepository playersRepository;
        private readonly IGameRoundsRepository gameRoundsRepository;
        private readonly IPlayerCardsRepository playerCardsRepository;
        private readonly IQuestionCardsRepository questionCardsRepository;

        public SubmitAnswerValidator(
            IPlayersRepository playersRepository,
            IGameRoundsRepository gameRoundsRepository,
            IPlayerCardsRepository playerCardsRepository,
            IQuestionCardsRepository questionCardsRepository)
        {
            this.playersRepository = playersRepository;
            this.gameRoundsRepository = gameRoundsRepository;
            this.playerCardsRepository = playerCardsRepository;
            this.questionCardsRepository = questionCardsRepository;
        }

        public async Task<ValidatorResult> Validate(SubmitAnswer request)
        {
            if (request.UserId == 0)
                return new ValidatorResult(ValidatorErrors.Games.UserIdRequired);
            if (request.GameId == 0)
                return new ValidatorResult(ValidatorErrors.Games.GameIdRequired);
            if (request.GameRoundId == 0)
                return new ValidatorResult(ValidatorErrors.Games.GameRoundIdRequired);
            if (request.PlayerCardId == 0)
                return new ValidatorResult(ValidatorErrors.Games.PlayerCardIdRequired);

            var questionCard = await questionCardsRepository.GetActiveQuestionCardForRound(request.GameRoundId);
            var player = await playersRepository.GetPlayerByUserId(request.UserId, request.GameId);
            if (player == null)
                return new ValidatorResult(ValidatorErrors.Players.UserNotLinkedWithPlayer);

            if (questionCard.NumberOfAnswers > 1)
            {
                if (!request.SecondPlayerCardId.HasValue)
                    return new ValidatorResult(ValidatorErrors.Games.SecondPlayerCardIdRequired);

                var playerCard = await playerCardsRepository.GetPlayerCard(request.SecondPlayerCardId.Value);

                if (playerCard == null || playerCard.PlayerId != player.PlayerId)
                    return new ValidatorResult(ValidatorErrors.Games.CardNotLinkedWithPlayer);
            }

            var round = await gameRoundsRepository.GetCurrentGameRound(request.GameId);
            if (round == null
                || round.GameRoundId != request.GameRoundId
                || round.Status != Common.Enums.GameRoundStatusEnum.InProgress)
                return new ValidatorResult(ValidatorErrors.Games.InvalidGameRoundStatus);

            return new ValidatorResult();
        }
    }
}

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

            if (await questionCardsRepository.IsQuestionCardMultiChoice(request.GameRoundId))
            {
                if (!request.SecondPlayerCardId.HasValue)
                    return new ValidatorResult(ValidatorErrors.Games.SecondPlayerCardIdRequired);

                if (request.SecondPlayerCardId.HasValue
                    && !await playerCardsRepository.IsCardLinkedWithUser(request.UserId, request.SecondPlayerCardId.Value))
                    return new ValidatorResult(ValidatorErrors.Games.CardNotLinkedWithPlayer);
            }

            if (!await playersRepository.IsUserLinkedWithPlayer(request.UserId, request.GameId))
                return new ValidatorResult(ValidatorErrors.Players.UserNotLinkedWithPlayer);
            if (!await gameRoundsRepository.IsRoundInGame(request.GameId, request.GameRoundId))
                return new ValidatorResult(ValidatorErrors.Games.RoundNotLinkedWithGame);

            if (!await gameRoundsRepository.IsGameRoundInProgress(request.GameRoundId))
                return new ValidatorResult(ValidatorErrors.Games.InvalidGameRoundStatus);
            if (!await playerCardsRepository.IsCardLinkedWithUser(request.UserId, request.PlayerCardId))
                return new ValidatorResult(ValidatorErrors.Games.CardNotLinkedWithPlayer);

            return new ValidatorResult();
        }
    }
}

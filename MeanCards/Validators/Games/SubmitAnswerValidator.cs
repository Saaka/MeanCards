﻿using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games
{
    public class SubmitAnswerValidator : IRequestValidator<SubmitAnswer>
    {
        private readonly IBaseGameRequestsValidator baseGameRequestsValidator;
        private readonly IPlayersRepository playersRepository;
        private readonly IGameRoundsRepository gameRoundsRepository;
        private readonly IPlayerCardsRepository playerCardsRepository;
        private readonly IQuestionCardsRepository questionCardsRepository;
        private readonly IPlayerAnswersRepository playerAnswersRepository;

        public SubmitAnswerValidator(
            IBaseGameRequestsValidator baseGameRequestsValidator,
            IPlayersRepository playersRepository,
            IGameRoundsRepository gameRoundsRepository,
            IPlayerCardsRepository playerCardsRepository,
            IQuestionCardsRepository questionCardsRepository,
            IPlayerAnswersRepository playerAnswersRepository)
        {
            this.baseGameRequestsValidator = baseGameRequestsValidator;
            this.playersRepository = playersRepository;
            this.gameRoundsRepository = gameRoundsRepository;
            this.playerCardsRepository = playerCardsRepository;
            this.questionCardsRepository = questionCardsRepository;
            this.playerAnswersRepository = playerAnswersRepository;
        }

        public async Task<ValidatorResult> Validate(SubmitAnswer request)
        {
            var baseResult = await baseGameRequestsValidator.Validate(request);
            if (!baseResult.IsSuccessful)
                return new ValidatorResult(baseResult.Error);

            var player = await playersRepository.GetPlayerByUserId(request.UserId, request.GameId);

            if (await playerAnswersRepository.HasPlayerSubmittedAnswer(player.PlayerId, request.GameRoundId))
                return new ValidatorResult(ValidatorErrors.Games.PlayerAlreadySubmittedAnswer);

            if (request.PlayerCardId == 0)
                return new ValidatorResult(ValidatorErrors.Games.PlayerCardIdRequired);

            var round = await gameRoundsRepository.GetGameRound(request.GameId, request.GameRoundId);
            if (round.Status != Common.Enums.GameRoundStatusEnum.InProgress)
                return new ValidatorResult(ValidatorErrors.Games.InvalidGameRoundStatus);

            var firstPlayerCard = await playerCardsRepository.GetPlayerCard(request.PlayerCardId);
            if (firstPlayerCard == null || firstPlayerCard.PlayerId != player.PlayerId)
                return new ValidatorResult(ValidatorErrors.Games.CardNotLinkedWithPlayer);

            var questionCard = await questionCardsRepository.GetActiveQuestionCardForRound(request.GameRoundId);
            if (questionCard.NumberOfAnswers > 1)
            {
                if (!request.SecondPlayerCardId.HasValue)
                    return new ValidatorResult(ValidatorErrors.Games.SecondPlayerCardIdRequired);

                var secondPlayerCard = await playerCardsRepository.GetPlayerCard(request.SecondPlayerCardId.Value);
                if (secondPlayerCard == null || secondPlayerCard.PlayerId != player.PlayerId)
                    return new ValidatorResult(ValidatorErrors.Games.CardNotLinkedWithPlayer);
            }

            return new ValidatorResult();
        }
    }
}

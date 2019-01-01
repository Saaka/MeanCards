﻿using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games
{
    public class StartGameRoundValidator : IRequestValidator<StartGameRound>
    {
        private readonly IBaseGameRequestsValidator baseGameRequestsValidator;
        private readonly IPlayersRepository playersRepository;
        private readonly IGameRoundsRepository gameRoundsRepository;
        private readonly IGamesRepository gamesRepository;

        public StartGameRoundValidator(
            IBaseGameRequestsValidator baseGameRequestsValidator,
            IPlayersRepository playersRepository,
            IGameRoundsRepository gameRoundsRepository,
            IGamesRepository gamesRepository)
        {
            this.baseGameRequestsValidator = baseGameRequestsValidator;
            this.playersRepository = playersRepository;
            this.gameRoundsRepository = gameRoundsRepository;
            this.gamesRepository = gamesRepository;
        }

        public async Task<ValidatorResult> Validate(StartGameRound request)
        {
            var baseResult = await baseGameRequestsValidator.Validate(request);
            if (!baseResult.IsSuccessful)
                return new ValidatorResult(baseResult.Error);
            
            var round = await gameRoundsRepository.GetGameRound(request.GameId, request.GameRoundId);
            if (round.Status != Common.Enums.GameRoundStatusEnum.Pending)
                return new ValidatorResult(ValidatorErrors.Games.InvalidGameRoundStatus);

            var game = await gamesRepository.GetGameById(request.GameId);
            var player = await playersRepository.GetPlayerByUserId(request.UserId, request.GameId);
            if (game.OwnerId != request.UserId && round.OwnerPlayerId != player.PlayerId)
                return new ValidatorResult(ValidatorErrors.Games.UserCantStartRound);

            return new ValidatorResult();
        }
    }
}

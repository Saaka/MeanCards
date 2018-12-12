using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.GameManagement.CoreServices;
using MeanCards.Model.Core.Games;
using MeanCards.Validators;
using System.Threading.Tasks;

namespace MeanCards.GameManagement
{
    public interface IStartGameRoundHandler
    {
        Task<StartGameRoundResult> StartGameRound(StartGameRound request);
    }

    public class StartGameRoundHandler : IStartGameRoundHandler
    {
        private readonly IRequestValidator<StartGameRound> validator;
        private readonly IGameRoundsRepository gameRoundsRepository;
        private readonly IGameCheckpointUpdater gameCheckpointUpdater;

        public StartGameRoundHandler(
            IRequestValidator<StartGameRound> validator,
            IGameRoundsRepository gameRoundsRepository,
            IGameCheckpointUpdater gameCheckpointUpdater)
        {
            this.validator = validator;
            this.gameRoundsRepository = gameRoundsRepository;
            this.gameCheckpointUpdater = gameCheckpointUpdater;
        }

        public async Task<StartGameRoundResult> StartGameRound(StartGameRound request)
        {
            var validatorResult = await validator.Validate(request);
            if (!validatorResult.IsSuccessful)
                return new StartGameRoundResult(validatorResult.Error);

            var started = await gameRoundsRepository.StartGameRound(request.GameRoundId);
            if (!started)
                return new StartGameRoundResult(GameErrors.GameRoundCouldNotBeStarted);
            
            var checkpoint = await gameCheckpointUpdater.Update(request.GameId);

            return new StartGameRoundResult();
        }
    }
}

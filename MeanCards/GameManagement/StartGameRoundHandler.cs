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

        public StartGameRoundHandler(IRequestValidator<StartGameRound> validator)
        {
            this.validator = validator;
        }

        public async Task<StartGameRoundResult> StartGameRound(StartGameRound request)
        {
            var validatorResult = await validator.Validate(request);
            if (!validatorResult.IsSuccessful)
                return new StartGameRoundResult(validatorResult.Error);


            return new StartGameRoundResult();
        }
    }
}

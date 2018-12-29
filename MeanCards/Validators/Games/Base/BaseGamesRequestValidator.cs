using MeanCards.Model.Core;
using MeanCards.Model.Core.Games.Base;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games.Base
{
    public class BaseGamesRequestValidator<T> : IRequestValidator<T>
    {
        private readonly IRequestValidator<IGameRequest> gameRequestValidator;
        private readonly IRequestValidator<IGameRoundRequest> gameRoundRequestValidator;
        private readonly IRequestValidator<IUserRequest> userRequestValidator;

        public BaseGamesRequestValidator(
            IRequestValidator<IGameRequest> gameRequestValidator,
            IRequestValidator<IGameRoundRequest> gameRoundRequestValidator, 
            IRequestValidator<IUserRequest> userRequestValidator)
        {
            this.gameRequestValidator = gameRequestValidator;
            this.gameRoundRequestValidator = gameRoundRequestValidator;
            this.userRequestValidator = userRequestValidator;
        }

        public async Task<ValidatorResult> Validate(T request)
        {
            if(request is IGameRequest)
            {
                var result = await gameRequestValidator.Validate(request as IGameRequest);
                if (!result.IsSuccessful)
                    return new ValidatorResult(result.Error);
            }
            if (request is IGameRoundRequest)
            {
                var result = await gameRoundRequestValidator.Validate(request as IGameRoundRequest);
                if (!result.IsSuccessful)
                    return new ValidatorResult(result.Error);
            }
            if (request is IUserRequest)
            {
                var result = await userRequestValidator.Validate(request as IUserRequest);
                if (!result.IsSuccessful)
                    return new ValidatorResult(result.Error);
            }

            return new ValidatorResult();
        }
    }
}

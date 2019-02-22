using MeanCards.Model.Core;
using MeanCards.Model.Core.Games.Base;
using MediatR;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games
{
    public interface IBaseGameRequestsValidator : IRequestValidator<IBaseRequest>
    {
    }

    public class BaseGameRequestsValidator : IBaseGameRequestsValidator
    {
        private readonly IRequestValidator<IGameRequest> gameRequestValidator;
        private readonly IRequestValidator<IGameRoundRequest> gameRoundRequestValidator;
        private readonly IRequestValidator<IUserRequest> userRequestValidator;
        private readonly IRequestValidator<IPlayerRequest> playerRequestValidator;

        public BaseGameRequestsValidator(
            IRequestValidator<IGameRequest> gameRequestValidator,
            IRequestValidator<IGameRoundRequest> gameRoundRequestValidator, 
            IRequestValidator<IUserRequest> userRequestValidator,
            IRequestValidator<IPlayerRequest> playerRequestValidator)
        {
            this.gameRequestValidator = gameRequestValidator;
            this.gameRoundRequestValidator = gameRoundRequestValidator;
            this.userRequestValidator = userRequestValidator;
            this.playerRequestValidator = playerRequestValidator;
        }

        public async Task<ValidatorResult> Validate(IBaseRequest request)
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
            if (request is IPlayerRequest)
            {
                var result = await playerRequestValidator.Validate(request as IPlayerRequest);
                if (!result.IsSuccessful)
                    return new ValidatorResult(result.Error);
            }

            return new ValidatorResult();
        }
    }
}

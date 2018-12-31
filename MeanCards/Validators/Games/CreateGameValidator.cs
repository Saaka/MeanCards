using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Games;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games
{
    public class CreateGameValidator : IRequestValidator<CreateGame>
    {
        private readonly IBaseGameRequestsValidator baseGameRequestsValidator;

        public CreateGameValidator(
            IBaseGameRequestsValidator baseGameRequestsValidator)
        {
            this.baseGameRequestsValidator = baseGameRequestsValidator;
        }

        public async Task<ValidatorResult> Validate(CreateGame request)
        {
            var baseResult = await baseGameRequestsValidator.Validate(request);
            if (!baseResult.IsSuccessful)
                return new ValidatorResult(baseResult.Error);

            if (string.IsNullOrEmpty(request.Name))
                return new ValidatorResult(ValidatorErrors.Games.GameNameRequired);
            if (request.LanguageId == 0)
                return new ValidatorResult(ValidatorErrors.Games.GameLanguageRequired);

            return new ValidatorResult();
        }
    }
}

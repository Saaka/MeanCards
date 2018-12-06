using MeanCards.Common.Constants;
using MeanCards.Model.Core.Games;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games
{
    public class CreateGameValidator : IRequestValidator<CreateGame>
    {
        public async Task<ValidatorResult> Validate(CreateGame request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return new ValidatorResult(ValidatorErrors.Games.GameNameRequired);
            if (request.LanguageId == 0)
                return new ValidatorResult(ValidatorErrors.Games.GameLanguageRequired);
            if (request.OwnerId == 0)
                return new ValidatorResult(ValidatorErrors.Games.GameOwnerRequired);

            return new ValidatorResult();
        }
    }
}

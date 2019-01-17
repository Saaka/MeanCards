using MeanCards.Model.Core.Games;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games
{
    public class SelectAnswerValidator : IRequestValidator<SelectAnswer>
    {
        private readonly IBaseGameRequestsValidator baseGameRequestsValidator;

        public SelectAnswerValidator(
            IBaseGameRequestsValidator baseGameRequestsValidator)
        {
            this.baseGameRequestsValidator = baseGameRequestsValidator;
        }

        public async Task<ValidatorResult> Validate(SelectAnswer request)
        {
            var baseResult = await baseGameRequestsValidator.Validate(request);
            if (!baseResult.IsSuccessful)
                return new ValidatorResult(baseResult.Error);
            
            return new ValidatorResult();
        }
    }
}

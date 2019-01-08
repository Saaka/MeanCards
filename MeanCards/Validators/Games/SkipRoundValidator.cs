using MeanCards.Model.Core.Games;
using System;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games
{
    public class SkipRoundValidator : IRequestValidator<SkipRound>
    {
        public async Task<ValidatorResult> Validate(SkipRound request)
        {
            throw new NotImplementedException();
        }
    }
}

using MeanCards.Model.Core.Games;
using System;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games
{
    public class EndGameValidator : IRequestValidator<EndGame>
    {
        public async Task<ValidatorResult> Validate(EndGame request)
        {
            throw new NotImplementedException();
        }
    }
}

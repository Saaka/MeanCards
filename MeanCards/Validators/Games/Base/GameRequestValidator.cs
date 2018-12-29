using MeanCards.Model.Core.Games.Base;
using System;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games.Base
{
    public class GameRequestValidator : IRequestValidator<IGameRequest>
    {
        public async Task<ValidatorResult> Validate(IGameRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

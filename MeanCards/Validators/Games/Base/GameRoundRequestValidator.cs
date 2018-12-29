using MeanCards.Model.Core.Games.Base;
using System;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games.Base
{
    public class GameRoundRequestValidator : IRequestValidator<IGameRoundRequest>
    {
        public async Task<ValidatorResult> Validate(IGameRoundRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

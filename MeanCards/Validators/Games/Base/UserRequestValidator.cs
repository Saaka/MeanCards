using MeanCards.Model.Core;
using System;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games.Base
{
    public class UserRequestValidator : IRequestValidator<IUserRequest>
    {
        public async Task<ValidatorResult> Validate(IUserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

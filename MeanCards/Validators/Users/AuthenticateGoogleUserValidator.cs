using MeanCards.Common.Constants;
using MeanCards.Model.Core.Users;
using System.Threading.Tasks;

namespace MeanCards.Validators.Users
{
    public class AuthenticateGoogleUserValidator : IRequestValidator<AuthenticateGoogleUser>
    {
        public async Task<ValidatorResult> Validate(AuthenticateGoogleUser request)
        {
            if (string.IsNullOrWhiteSpace(request.GoogleId))
                return new ValidatorResult(ValidatorErrors.Users.GoogleIdRequired);

            if (string.IsNullOrEmpty(request.DisplayName))
                return new ValidatorResult(ValidatorErrors.Users.UserNameRequired);

            if (string.IsNullOrEmpty(request.Email))
                return new ValidatorResult(ValidatorErrors.Users.UserEmailRequired);

            return new ValidatorResult();
        }
    }
}

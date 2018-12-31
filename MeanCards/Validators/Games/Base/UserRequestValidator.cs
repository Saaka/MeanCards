using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core;
using System.Threading.Tasks;

namespace MeanCards.Validators.Games.Base
{
    public class UserRequestValidator : IRequestValidator<IUserRequest>
    {
        private readonly IUsersRepository usersRepository;

        public UserRequestValidator(
            IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<ValidatorResult> Validate(IUserRequest request)
        {
            if (request.UserId == 0)
                return new ValidatorResult(ValidatorErrors.Games.UserIdRequired);

            if (!await usersRepository.ActiveUserExists(request.UserId))
                return new ValidatorResult(ValidatorErrors.Users.UserIdNotFound);

            return new ValidatorResult();
        }
    }
}

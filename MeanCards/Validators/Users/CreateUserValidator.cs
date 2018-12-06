using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Users;
using System.Threading.Tasks;

namespace MeanCards.Validators.Users
{
    public class CreateUserValidator : IRequestValidator<CreateUser>
    {
        private readonly IUsersRepository usersRepository;

        public CreateUserValidator(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<ValidatorResult> Validate(CreateUser request)
        {
            if (string.IsNullOrEmpty(request.DisplayName))
                return new ValidatorResult(ValidatorErrors.Users.UserNameRequired);

            if (string.IsNullOrEmpty(request.Email))
                return new ValidatorResult(ValidatorErrors.Users.UserEmailRequired);

            if (string.IsNullOrEmpty(request.Password))
                return new ValidatorResult(ValidatorErrors.Users.UserPasswordRequired);

            if (request.Password.Length < AuthConstants.MinPasswordLength)
                return new ValidatorResult(ValidatorErrors.Users.UserPasswordTooShort);

            if (await usersRepository.UserEmailExists(request.Email))
                return new ValidatorResult(ValidatorErrors.Users.DuplicatedUserEmail);

            if (await usersRepository.UserNameExists(request.DisplayName))
                return new ValidatorResult(ValidatorErrors.Users.DuplicatedUserName);

            return new ValidatorResult();
        }
    }
}

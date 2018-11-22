using MeanCards.Commands.Users;
using MeanCards.Common.Constants;
using MeanCards.DAL.Interfaces.Repository;
using System.Threading.Tasks;

namespace MeanCards.Validators.User
{
    public class CreateUserValidator : ICommandValidator<CreateUser>
    {
        private readonly IUsersRepository usersRepository;

        public CreateUserValidator(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<ValidatorResult> Validate(CreateUser request)
        {
            if (string.IsNullOrEmpty(request.DisplayName))
                return new ValidatorResult(ValidatorErrors.NameRequired);

            if (string.IsNullOrEmpty(request.Email))
                return new ValidatorResult(ValidatorErrors.EmailRequired);

            if (string.IsNullOrEmpty(request.Password))
                return new ValidatorResult(ValidatorErrors.PasswordRequired);

            if (request.Password.Length < AuthConstants.MinPasswordLength)
                return new ValidatorResult(ValidatorErrors.PasswordTooShort);

            if (await usersRepository.UserEmailExists(request.Email))
                return new ValidatorResult(ValidatorErrors.DuplicatedEmail);

            if (await usersRepository.UserNameExists(request.DisplayName))
                return new ValidatorResult(ValidatorErrors.DuplicatedUserName);

            return new ValidatorResult();
        }
    }
}

using MeanCards.Commands.Users;
using MeanCards.Common.ProfileImageUrlProvider;
using MeanCards.Common.RandomCodeProvider;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Validators;
using System.Threading.Tasks;

namespace MeanCards.UserManagement
{
    public interface ICreateUserHandler
    {
        Task<CreateUserResult> Handle(CreateUser command);
    }

    public class CreateUserHandler : ICreateUserHandler
    {
        private readonly ICodeGenerator codeGenerator;
        private readonly ICommandValidator<CreateUser> commandValidator;
        private readonly IUsersRepository usersRepository;
        private readonly IProfileImageUrlProvider imageUrlProvider;

        public CreateUserHandler(ICodeGenerator codeGenerator,
            ICommandValidator<CreateUser> requestValidator,
            IUsersRepository usersRepository,
            IProfileImageUrlProvider imageUrlProvider)
        {
            this.codeGenerator = codeGenerator;
            this.commandValidator = requestValidator;
            this.usersRepository = usersRepository;
            this.imageUrlProvider = imageUrlProvider;
        }

        public async Task<CreateUserResult> Handle(CreateUser command)
        {
            var validationResult = await commandValidator.Validate(command);
            if (!validationResult.IsSuccessful)
                return new CreateUserResult(validationResult.Error);

            var userCode = codeGenerator.Generate();
            var imageUrl = imageUrlProvider.GetImageUrl(command.Email);

            var userId = await usersRepository.CreateUser(new Model.Creation.Users.CreateUserModel
            {
                Email = command.Email,
                DisplayName = command.DisplayName,
                Password = command.Password,
                Code = userCode,
                ImageUrl = imageUrl,
            });

            return new CreateUserResult
            {
                UserId = userId,
                Email = command.Email,
                DisplayName = command.DisplayName,
                Code = userCode,
                ImageUrl = imageUrl
            };
        }
    }
}

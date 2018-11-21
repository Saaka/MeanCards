using MeanCards.Commands.Users;
using MeanCards.Common.ProfileImageUrlProvider;
using MeanCards.Common.RandomCodeProvider;
using MeanCards.DAL.Interfaces.Repository;
using System.Threading.Tasks;

namespace MeanCards.UserManagement
{
    public interface ICreateUserHandler
    {
        Task<CreateUserResult> Handle(CreateUser command);
    }

    public class CreateUserHandler : ICreateUserHandler
    {
        protected readonly ICodeGenerator codeGenerator;
        protected readonly IUsersRepository usersRepository;
        private readonly IProfileImageUrlProvider imageUrlProvider;

        public CreateUserHandler(ICodeGenerator codeGenerator,
            IUsersRepository usersRepository,
            IProfileImageUrlProvider imageUrlProvider)
        {
            this.codeGenerator = codeGenerator;
            this.usersRepository = usersRepository;
            this.imageUrlProvider = imageUrlProvider;
        }

        public async Task<CreateUserResult> Handle(CreateUser command)
        {
            var userCode = codeGenerator.Generate();
            var imageUrl = imageUrlProvider.GetImageUrl(command.Email);

            var userId = await usersRepository.CreateUser(new Model.Creation.Users.CreateUserModel
            {
                Email = command.Email,
                DisplayName = command.DisplayName,
                Password = command.Password,
                UserCode = userCode,
                ImageUrl = imageUrl,
            });

            return new CreateUserResult
            {
                UserId = userId,
                Email = command.Email,
                DisplayName = command.DisplayName,
                UserCode = userCode,
                ImageUrl = imageUrl
            };
        }
    }
}

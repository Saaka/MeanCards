using MeanCards.Common.ProfileImageUrlProvider;
using MeanCards.Common.RandomCodeProvider;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Users;
using MeanCards.Model.DAL.Creation.Users;
using MeanCards.Model.DTO.Users;
using MeanCards.Validators;
using System.Threading.Tasks;

namespace MeanCards.UserManagement
{
    public interface ICreateUserHandler
    {
        Task<CreateUserResult> Handle(CreateUser request);
    }

    public class CreateUserHandler : ICreateUserHandler
    {
        private readonly ICodeGenerator codeGenerator;
        private readonly IRequestValidator<CreateUser> requestValidator;
        private readonly IUsersRepository usersRepository;
        private readonly IProfileImageUrlProvider imageUrlProvider;

        public CreateUserHandler(ICodeGenerator codeGenerator,
            IRequestValidator<CreateUser> requestValidator,
            IUsersRepository usersRepository,
            IProfileImageUrlProvider imageUrlProvider)
        {
            this.codeGenerator = codeGenerator;
            this.requestValidator = requestValidator;
            this.usersRepository = usersRepository;
            this.imageUrlProvider = imageUrlProvider;
        }

        public async Task<CreateUserResult> Handle(CreateUser request)
        {
            var validationResult = await requestValidator.Validate(request);
            if (!validationResult.IsSuccessful)
                return new CreateUserResult(validationResult.Error);

            var userCode = codeGenerator.Generate();
            var imageUrl = imageUrlProvider.GetImageUrl(request.Email);

            var result = await usersRepository.CreateUser(new CreateUserModel
            {
                Email = request.Email,
                DisplayName = request.DisplayName,
                Password = request.Password,
                Code = userCode,
                ImageUrl = imageUrl,
            });
            if (!result.IsSuccessful)
                return new CreateUserResult(result.Error);

            return new CreateUserResult
            {
                User = new UserModel
                {
                    UserId = result.Model.UserId,
                    Email = result.Model.Email,
                    DisplayName = result.Model.DisplayName,
                    UserName = result.Model.UserName,
                    Code = result.Model.Code,
                    ImageUrl = result.Model.ImageUrl
                }
            };
        }
    }
}

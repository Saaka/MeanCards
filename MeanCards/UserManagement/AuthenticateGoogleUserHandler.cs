using MeanCards.Common.RandomCodeProvider;
using MeanCards.DAL.Interfaces.Repository;
using MeanCards.Model.Core.Users;
using MeanCards.Model.DAL.Creation.Users;
using MeanCards.Model.DAL.Modification.Users;
using MeanCards.Model.DTO.Users;
using MeanCards.Validators;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MeanCards.UserManagement
{
    public interface IAuthenticateGoogleUserHandler : IRequestHandler<AuthenticateGoogleUser, CreateUserResult>
    {
    }

    public class AuthenticateGoogleUserHandler : IAuthenticateGoogleUserHandler
    {
        private readonly IRequestValidator<AuthenticateGoogleUser> requestValidator;
        private readonly IUsersRepository usersRepository;
        private readonly ICodeGenerator codeGenerator;

        public AuthenticateGoogleUserHandler(
            IRequestValidator<AuthenticateGoogleUser> requestValidator,
            IUsersRepository usersRepository,
            ICodeGenerator codeGenerator)
        {
            this.requestValidator = requestValidator;
            this.usersRepository = usersRepository;
            this.codeGenerator = codeGenerator;
        }

        public async Task<CreateUserResult> Handle(AuthenticateGoogleUser request, CancellationToken cancellationToken)
        {
            var validationResult = await requestValidator.Validate(request);
            if (!validationResult.IsSuccessful)
                return new CreateUserResult(validationResult.Error);

            if(await usersRepository.GoogleUserExists(request.Email, request.GoogleId))
            {
                return await UpdateExistingGoogleUser(request);
            }
            else
            {
                if(await usersRepository.UserEmailExists(request.Email))
                {
                    return await AddGoogleLoginToExistingUser(request);
                }
                else
                {
                    return await CreateNewGoogleUser(request);
                }
            }
        }

        private async Task<CreateUserResult> AddGoogleLoginToExistingUser(AuthenticateGoogleUser request)
        {
            var result = await usersRepository.MergeUserWithGoogle(new MergeUserWithGoogleModel
            {
                Email = request.Email,
                GoogleId = request.GoogleId
            });

            if(!result.IsSuccessful)
                return new CreateUserResult(result.Error);

            return CreateResult(result.Model);
        }

        private async Task<CreateUserResult> CreateNewGoogleUser(AuthenticateGoogleUser request)
        {
            var userCode = codeGenerator.Generate();
            var result = await usersRepository.CreateGoogleUser(new CreateGoogleUserModel
            {
                Code = userCode,
                Email = request.Email,
                DisplayName = request.DisplayName,
                GoogleId = request.GoogleId,
                ImageUrl = request.ImageUrl
            });

            if (!result.IsSuccessful)
                return new CreateUserResult(result.Error);

            return CreateResult(result.Model);
        }

        private async Task<CreateUserResult> UpdateExistingGoogleUser(AuthenticateGoogleUser request)
        {
            var result = await usersRepository.UpdateGoogleUser(new UpdateGoogleUserModel
            {
                Email = request.Email,
                ImageUrl = request.ImageUrl
            });

            if (!result.IsSuccessful)
                return new CreateUserResult(result.Error);

            return CreateResult(result.Model);
        }

        private CreateUserResult CreateResult(UserModel user)
        {
            return new CreateUserResult
            {
                User = user
            };
        }
    }
}

using MeanCards.UserManagement;
using MeanCards.ViewModel.Auth;
using MeanCards.WebAPI.Services.Validators;
using System.Threading.Tasks;

namespace MeanCards.WebAPI.Services
{
    public interface IAuthenticateService
    {
        Task<AuthenticateUserResult> RegisterUser(RegisterUserRequest request);
    }
    public  class AuthenticateService : IAuthenticateService
    {
        private readonly IJwtTokenFactory tokenFactory;
        private readonly IRequestValidator<RegisterUserRequest> registrationValidator;
        private readonly ICreateUserHandler createUserHandler;

        public AuthenticateService(IJwtTokenFactory tokenFactory,
            IRequestValidator<RegisterUserRequest> registrationValidator,
            ICreateUserHandler createUserHandler)
        {
            this.tokenFactory = tokenFactory;
            this.registrationValidator = registrationValidator;
            this.createUserHandler = createUserHandler;
        }

        public async Task<AuthenticateUserResult> RegisterUser(RegisterUserRequest request)
        {
            var validationResult = await registrationValidator.Validate(request);
            if (!validationResult.IsSuccessful)
                return new AuthenticateUserResult(validationResult.Error);

            var user = await createUserHandler.Handle(new Commands.Users.CreateUser
            {
                Email = request.Email,
                DisplayName = request.DisplayName,
                Password = request.Password
            });

            var token = tokenFactory.GenerateEncodedToken(user.DisplayName);

            return new AuthenticateUserResult
            {
                ImageUrl = user.ImageUrl,
                UserCode = user.UserCode,
                Token = token,
                UserName = user.DisplayName,
                Email = user.Email
            };
        }

        public async Task<AuthenticateUserResult> AuthenticateUser(AuthenticateUserRequest request)
        {

            return null;
        }

        public async Task<AuthenticateUserResult> AuthenticateGoogleToken(AuthenticateUserWithGoogleRequest request)
        {

            return null;
        }
    }
}

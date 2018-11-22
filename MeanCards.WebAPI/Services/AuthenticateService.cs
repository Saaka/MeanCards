using MeanCards.UserManagement;
using MeanCards.ViewModel.Auth;
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
        private readonly ICreateUserHandler createUserHandler;

        public AuthenticateService(IJwtTokenFactory tokenFactory,
            ICreateUserHandler createUserHandler)
        {
            this.tokenFactory = tokenFactory;
            this.createUserHandler = createUserHandler;
        }

        public async Task<AuthenticateUserResult> RegisterUser(RegisterUserRequest request)
        {
            var userResult = await createUserHandler.Handle(new Commands.Users.CreateUser
            {
                Email = request.Email,
                DisplayName = request.DisplayName,
                Password = request.Password
            });
            if (!userResult.IsSuccessful)
                return new AuthenticateUserResult(userResult.Error);

            var token = tokenFactory.GenerateEncodedToken(userResult.DisplayName);

            return new AuthenticateUserResult
            {
                ImageUrl = userResult.ImageUrl,
                UserCode = userResult.UserCode,
                Token = token,
                UserName = userResult.DisplayName,
                Email = userResult.Email
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

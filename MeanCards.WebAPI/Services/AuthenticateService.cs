using MeanCards.Model.Core.Users;
using MeanCards.UserManagement;
using MeanCards.ViewModel.Auth;
using System.Threading.Tasks;

namespace MeanCards.WebAPI.Services
{
    public interface IAuthenticateService
    {
        Task<AuthenticateUserResult> RegisterUser(RegisterUserRequest request);
        Task<AuthenticateUserResult> AuthenticateUser(AuthenticateUserRequest request);
        Task<AuthenticateUserResult> AuthenticateGoogleToken(AuthenticateUserWithGoogleRequest request);
    }

    public  class AuthenticateService : IAuthenticateService
    {
        private readonly IJwtTokenFactory tokenFactory;
        private readonly ICreateUserHandler createUserHandler;
        private readonly IGetUserByCredentialsHandler getUserByCredentialsHandler;

        public AuthenticateService(IJwtTokenFactory tokenFactory,
            ICreateUserHandler createUserHandler,
            IGetUserByCredentialsHandler getUserByCredentialsHandler)
        {
            this.tokenFactory = tokenFactory;
            this.createUserHandler = createUserHandler;
            this.getUserByCredentialsHandler = getUserByCredentialsHandler;
        }

        public async Task<AuthenticateUserResult> RegisterUser(RegisterUserRequest request)
        {
            var result = await createUserHandler.Handle(new CreateUser
            {
                Email = request.Email,
                DisplayName = request.DisplayName,
                Password = request.Password
            });
            if (!result.IsSuccessful)
                return new AuthenticateUserResult(result.Error);

            var token = tokenFactory.GenerateEncodedToken(result.User.Code);

            return new AuthenticateUserResult
            {
                Token = token,
                Email = result.User.Email,
                Name = result.User.DisplayName,
                Code = result.User.Code,
                ImageUrl = result.User.ImageUrl,
            };
        }

        public async Task<AuthenticateUserResult> AuthenticateUser(AuthenticateUserRequest request)
        {
            var result = await getUserByCredentialsHandler.Handle(new GetUserByCredentials
            {
                Email = request.Email,
                Password = request.Password
            });
            if (!result.IsSuccessful)
                return new AuthenticateUserResult(result.Error);

            var token = tokenFactory.GenerateEncodedToken(result.User.Code);

            return new AuthenticateUserResult
            {
                Token = token,
                Email = result.User.Email,
                Name = result.User.DisplayName,
                Code = result.User.Code,
                ImageUrl = result.User.ImageUrl,
            };
        }

        public async Task<AuthenticateUserResult> AuthenticateGoogleToken(AuthenticateUserWithGoogleRequest request)
        {

            return null;
        }
    }
}

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

        public AuthenticateService(IJwtTokenFactory tokenFactory,
            ICreateUserHandler createUserHandler)
        {
            this.tokenFactory = tokenFactory;
            this.createUserHandler = createUserHandler;
        }

        public async Task<AuthenticateUserResult> RegisterUser(RegisterUserRequest request)
        {
            var userResult = await createUserHandler.Handle(new CreateUser
            {
                Email = request.Email,
                DisplayName = request.DisplayName,
                Password = request.Password
            });
            if (!userResult.IsSuccessful)
                return new AuthenticateUserResult(userResult.Error);

            var token = tokenFactory.GenerateEncodedToken(userResult.Code);

            return new AuthenticateUserResult
            {
                Token = token,
                Email = userResult.Email,
                Name = userResult.DisplayName,
                Code = userResult.Code,
                ImageUrl = userResult.ImageUrl,
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

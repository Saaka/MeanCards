using MeanCards.Model.Core.Users;
using MeanCards.UserManagement;
using MeanCards.ViewModel.Auth;
using MeanCards.WebAPI.Services.Google;
using System;
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
        private readonly IAuthenticateGoogleUserHandler authenticateGoogleUserHandler;
        private readonly IGoogleTokenVerificationService googleTokenVerificationService;

        public AuthenticateService(IJwtTokenFactory tokenFactory,
            ICreateUserHandler createUserHandler,
            IGetUserByCredentialsHandler getUserByCredentialsHandler,
            IAuthenticateGoogleUserHandler authenticateGoogleUserHandler,
            IGoogleTokenVerificationService googleTokenVerificationService)
        {
            this.tokenFactory = tokenFactory;
            this.createUserHandler = createUserHandler;
            this.getUserByCredentialsHandler = getUserByCredentialsHandler;
            this.authenticateGoogleUserHandler = authenticateGoogleUserHandler;
            this.googleTokenVerificationService = googleTokenVerificationService;
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
            var googleResult = await googleTokenVerificationService.Validate(request.GoogleToken);
            if (!googleResult.IsSuccessful)
                return new AuthenticateUserResult(googleResult.Error);

            var result = await authenticateGoogleUserHandler.Handle(new AuthenticateGoogleUser
            {
                GoogleId = googleResult.TokenInfo.GoogleUserId,
                DisplayName = googleResult.TokenInfo.DisplayName,
                Email = googleResult.TokenInfo.Email,
                ImageUrl = googleResult.TokenInfo.ImageUrl
            });
            if (!result.IsSuccessful)
                return new AuthenticateUserResult(result.Error);

            var token = tokenFactory.GenerateEncodedToken(result.User.Code);

            return new AuthenticateUserResult
            {
                Code = result.User.Code,
                Email = result.User.Email,
                ImageUrl = result.User.ImageUrl,
                Name = result.User.DisplayName,
                Token = token
            };
        }
    }
}

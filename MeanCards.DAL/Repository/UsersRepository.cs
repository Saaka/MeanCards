using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Storage;
using MeanCards.DAL.Entity;
using System.Threading.Tasks;
using MeanCards.Model.DAL.Creation.Users;
using Microsoft.AspNetCore.Identity;
using System;
using MeanCards.Model.DAL.Access.Users;
using MeanCards.Model.DTO.Users;
using Microsoft.EntityFrameworkCore;
using MeanCards.Model.DAL.Modification.Users;
using MeanCards.Model.DAL;
using MeanCards.Common.Constants;

namespace MeanCards.DAL.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext context;
        private readonly UserManager<User> userManager;

        public UsersRepository(AppDbContext context,
            UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<RepositoryResult<UserModel>> CreateUser(CreateUserModel model)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.DisplayName,
                Code = model.Code,
                ImageUrl = model.ImageUrl,
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new RepositoryResult<UserModel>(result.Errors.ToString());

            return CreateUserResult(user);
        }

        public async Task<RepositoryResult<UserModel>> CreateGoogleUser(CreateGoogleUserModel model)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.DisplayName,
                Code = model.Code,
                ImageUrl = model.ImageUrl,
                GoogleId = model.GoogleId
            };
            var result = await userManager.CreateAsync(user);
            if (!result.Succeeded)
                return new RepositoryResult<UserModel>(result.Errors.ToString());

            return CreateUserResult(user);
        }

        public async Task<RepositoryResult<UserModel>> UpdateGoogleUser(UpdateGoogleUserModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return new RepositoryResult<UserModel>(AccessErrors.UserNotFound);

            user.ImageUrl = model.ImageUrl;
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
                return CreateUserResult(user);
            else
                return new RepositoryResult<UserModel>(result.Errors.ToString());
        }

        public async Task<RepositoryResult<UserModel>> MergeUserWithGoogle(MergeUserWithGoogleModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return new RepositoryResult<UserModel>(AccessErrors.UserNotFound);

            user.GoogleId = model.GoogleId;
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
                return CreateUserResult(user);
            else
                return new RepositoryResult<UserModel>(result.Errors.ToString());
        }

        public async Task<RepositoryResult<UserModel>> GetUserByCredentials(GetUserByCredentialsModel credentials)
        {
            var user = await userManager.FindByEmailAsync(credentials.Email);
            if (user == null)
                return new RepositoryResult<UserModel>(AccessErrors.UserNotFound);

            if (await userManager.CheckPasswordAsync(user, credentials.Password))
                return CreateUserResult(user);
            else
                return new RepositoryResult<UserModel>(AccessErrors.InvalidUserCredentials);
        }

        public async Task<bool> GoogleUserExists(string email, string googleId)
        {
            var normalizedEmail = GetNormalizedEmail(email);

            var userExists = await context.Users
                .AnyAsync(x => x.NormalizedEmail == normalizedEmail && x.GoogleId == googleId);

            return userExists;
        }

        public async Task<bool> UserEmailExists(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            return user != null;
        }

        public async Task<bool> UserNameExists(string name)
        {
            var user = await userManager.FindByNameAsync(name);

            return user != null;
        }

        public async Task<bool> ActiveUserExists(int userId)
        {
            var userExists = await context.Users
                .AnyAsync(x => x.Id == userId);

            return userExists;
        }

        private string GetNormalizedEmail(string email)
        {
            return email.ToUpper();
        }

        private RepositoryResult<UserModel> CreateUserResult(User user)
        {
            return new RepositoryResult<UserModel>
            {
                Model = new UserModel
                {
                    UserId = user.Id,
                    Code = user.Code,
                    Email = user.Email,
                    DisplayName = user.UserName,
                    ImageUrl = user.ImageUrl
                }
            };
        }
    }
}

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

        public async Task<UserModel> CreateUser(CreateUserModel model)
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
                throw new ArgumentException(result.ToString()); // TEMP FOR DEBUG

            return CreateUserDto(user);
        }

        public async Task<UserModel> CreateGoogleUser(CreateGoogleUserModel model)
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
                throw new ArgumentException(result.ToString()); // TEMP FOR DEBUG

            return CreateUserDto(user);
        }

        public async Task<UserModel> UpdateGoogleUser(UpdateGoogleUserModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return null;

            user.ImageUrl = model.ImageUrl;
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
                return CreateUserDto(user);
            else
                return null;
        }

        public async Task<UserModel> MergeUserWithGoogle(MergeUserWithGoogleModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return null;

            user.GoogleId = model.GoogleId;
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
                return CreateUserDto(user);
            else
                return null;
        }

        public async Task<UserModel> GetUserByCredentials(GetUserByCredentialsModel credentials)
        {
            var user = await userManager.FindByEmailAsync(credentials.Email);
            if (user == null)
                return null;

            if (await userManager.CheckPasswordAsync(user, credentials.Password))
            {
                return CreateUserDto(user);
            }

            return null;
        }

        public async Task<bool> GoogleUserExists(string email, string googleId)
        {
            var normalizedEmail = GetNormalizedEmail(email);

            var userExists = await context.Users
                .AnyAsync(x => x.NormalizedEmail == email && x.GoogleId == googleId);

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

        private string GetNormalizedEmail(string email)
        {
            return email.ToUpper();
        }

        private UserModel CreateUserDto(User user)
        {
            return new UserModel
            {
                UserId = user.Id,
                Code = user.Code,
                Email = user.Email,
                DisplayName = user.UserName,
                ImageUrl = user.ImageUrl
            };
        }
    }
}

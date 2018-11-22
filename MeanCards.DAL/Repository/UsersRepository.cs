using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Storage;
using MeanCards.DataModel.Entity;
using System.Threading.Tasks;
using MeanCards.Model.Creation.Users;
using Microsoft.AspNetCore.Identity;
using System;

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

        public async Task<int> CreateUser(CreateUserModel model)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.DisplayName,
                UserCode = model.UserCode,
                ImageUrl = model.ImageUrl,
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                throw new ArgumentException(result.ToString()); // TEMP FOR DEBUG

            return user.Id;
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
    }
}

using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Storage;
using MeanCards.DataModel.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MeanCards.Model.Creation.Users;
using System;

namespace MeanCards.DAL.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext context;

        public UsersRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<int> CreateUser(CreateUserModel model)
        {
            var newUser = AddUserToContext(model);
            var auth = new UserAuth
            {
                Password = model.Password,
                DisplayName = model.DisplayName,
                ImageUrl = model.ImageUrl,
                CreateDate = DateTime.UtcNow
            };
            newUser.UserAuths.Add(auth);

            await context.SaveChangesAsync();

            return newUser.UserId;
        }

        public async Task<int> CreateUser(CreateGoogleUserModel model)
        {
            var newUser = AddUserToContext(model);
            var auth = new UserGoogleAuth
            {
                GoogleId = model.GoogleId,
                DisplayName = model.DisplayName,
                ImageUrl = model.ImageUrl,
                CreateDate = DateTime.UtcNow
            };
            newUser.UserGoogleAuths.Add(auth);

            await context.SaveChangesAsync();

            return newUser.UserId;
        }

        private User AddUserToContext(CreateUserModelBase model)
        {
            var newUser = new User
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                ImageUrl = model.ImageUrl,
                IsActive = true
            };
            context.Users.Add(newUser);

            return newUser;
        }

        public async Task<UserGoogleAuth> GetGoogleUser(string googleId)
        {
            var query = from user in context.UserGoogleAuths
                        where user.GoogleId == googleId
                        select user;

            return await query.FirstOrDefaultAsync();
        }

        public async Task<UserAuth> GetUserAuth(string email)
        {
            var query = from ua in context.UserAuths
                        join u in context.Users on ua.UserId equals u.UserId
                        where u.Email == email
                        select ua;

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetAllActiveUsers()
        {
            var query = from user in context.Users
                        where user.IsActive == true
                        select user;

            return await query.ToListAsync();
        }
    }
}

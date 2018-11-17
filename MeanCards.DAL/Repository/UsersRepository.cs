﻿using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Storage;
using MeanCards.DataModel.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MeanCards.Model.Creation.Users;
using System.Transactions;

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
            var newUser = new User
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                ImageUrl = model.ImageUrl,
                IsActive = true
            };
            context.Users.Add(newUser);
            await context.SaveChangesAsync();

            return newUser.UserId;
        }

        public Task<int> CreateUser(CreateGoogleUserModel model)
        {
            throw new System.NotImplementedException();
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

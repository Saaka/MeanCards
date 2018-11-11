using MeanCards.DAL.Interfaces.Repository;
using MeanCards.DAL.Storage;
using MeanCards.DataModel.Entity;
using MeanCards.Model.Creation;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MeanCards.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<int> CreateUser(CreateUserModel model)
        {
            var newUser = new User
            {
                DisplayName = model.DisplayName,
                IsActive = true
            };

            context.Users.Add(newUser);
            await context.SaveChangesAsync();

            return newUser.UserId;
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

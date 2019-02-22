using MeanCards.Model.Core.Users;
using MeanCards.Model.DTO.Users;
using MeanCards.UserManagement;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace MeanCards.WebAPI.Services
{
    public interface IUserDataService
    {
        Task<UserModel> GetUserData(string userCode);
    }

    public class UserDataService : IUserDataService
    {
        private readonly IMemoryCache memoryCache;
        private readonly IGetUserByCodeHandler getUserByCodeHandler;
        private const string ContextUserCachePrefix = "USR_CTX_";

        public UserDataService(
            IMemoryCache memoryCache,
            IGetUserByCodeHandler getUserByCodeHandler)
        {
            this.memoryCache = memoryCache;
            this.getUserByCodeHandler = getUserByCodeHandler;
        }

        public async Task<UserModel> GetUserData(string userCode)
        {
            var cacheKey = $"{ContextUserCachePrefix}{userCode}";

            var userData = await memoryCache.GetOrCreateAsync(cacheKey, async (ce) =>
            {
                ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                ce.AbsoluteExpiration = DateTime.Now.AddHours(1);

                var result = await getUserByCodeHandler.Handle(new GetUserByCode { UserCode = userCode });
                if (!result.IsSuccessful)
                    throw new ArgumentException(result.Error);

                return result.User;
            });

            return userData;
        }
    }
}

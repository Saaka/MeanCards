using MeanCards.Model.Core.Users;
using MeanCards.Model.DTO.Users;
using MeanCards.UserManagement;
using MediatR;
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
        private readonly IMediator mediator;
        private const string ContextUserCachePrefix = "USR_CTX_";

        public UserDataService(
            IMemoryCache memoryCache,
            IMediator mediator)
        {
            this.memoryCache = memoryCache;
            this.mediator = mediator;
        }

        public async Task<UserModel> GetUserData(string userCode)
        {
            var cacheKey = $"{ContextUserCachePrefix}{userCode}";

            var userData = await memoryCache.GetOrCreateAsync(cacheKey, async (ce) =>
            {
                ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                ce.AbsoluteExpiration = DateTime.Now.AddHours(1);

                var result = await mediator
                    .Send(new GetUserByCode { UserCode = userCode });
                if (!result.IsSuccessful)
                    throw new ArgumentException(result.Error);

                return result.User;
            });

            return userData;
        }
    }
}

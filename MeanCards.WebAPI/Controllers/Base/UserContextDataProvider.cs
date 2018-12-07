using MeanCards.Model.DTO.Users;
using MeanCards.WebAPI.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace MeanCards.WebAPI.Controllers.Base
{
    public interface IUserContextDataProvider
    {
        Task<UserModel> GetUser(HttpContext context);
    }
    public class UserContextDataProvider : IUserContextDataProvider
    {
        private const string SubClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        private readonly IUserDataService userDataService;

        public UserContextDataProvider(IUserDataService userDataService)
        {
            this.userDataService = userDataService;
        }

        public async Task<UserModel> GetUser(HttpContext context)
        {
            if (context.User == null 
                || context.User.Claims == null 
                || !context.User.HasClaim(x => x.Type == SubClaimType))
                throw new InvalidOperationException("Can't authenticate current user");

            var userCode = context.User.FindFirst(x => x.Type == SubClaimType).Value;

            var userData = await userDataService.GetUserData(userCode);

            return userData;
        }
    }
}

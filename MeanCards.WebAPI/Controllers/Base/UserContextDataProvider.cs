using Microsoft.AspNetCore.Http;
using System;

namespace MeanCards.WebAPI.Controllers.Base
{
    public interface IUserContextDataProvider
    {
        string GetUserCode(HttpContext context);
    }
    public class UserContextDataProvider : IUserContextDataProvider
    {
        private const string SubClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        public string GetUserCode(HttpContext context)
        {
            if (context.User == null 
                || context.User.Claims == null 
                || !context.User.HasClaim(x => x.Type == SubClaimType))
                throw new InvalidOperationException("Can't authenticate current user");

            return context.User.FindFirst(x => x.Type == SubClaimType).Value;
        }
    }
}

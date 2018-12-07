using MeanCards.Model.DTO.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MeanCards.WebAPI.Controllers.Base
{
    public class ControllerAuthBase : ControllerBase
    {
        protected IUserContextDataProvider UserContextDataProvider { get; }

        public ControllerAuthBase(IUserContextDataProvider userContextDataProvider)
        {
            UserContextDataProvider = userContextDataProvider;
        }

        protected async virtual Task<UserModel> GetUserData()
        {
            return await UserContextDataProvider.GetUser(HttpContext);
        }
    }
}

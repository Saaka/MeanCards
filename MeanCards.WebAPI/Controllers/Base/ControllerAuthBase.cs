using MeanCards.Model.Core;
using MeanCards.Model.DTO.Users;
using Microsoft.AspNetCore.Mvc;
using System;
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

        protected ActionResult CreateResult<T>(BaseResult @base, Func<T> result)
            where T: class, new ()
        {
            if (@base.IsSuccessful)
            {
                return Ok(result());
            }
            else
            {
                return BadRequest(@base.Error);
            }
        }

        protected ActionResult CreateResult<T>(BaseResult @base)
            where T : class, new()
        {
            if (@base.IsSuccessful)
            {
                return Ok(new T());
            }
            else
            {
                return BadRequest(@base.Error);
            }
        }
    }
}

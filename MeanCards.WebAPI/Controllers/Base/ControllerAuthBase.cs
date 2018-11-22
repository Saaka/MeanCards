using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        protected virtual string UserCode => UserContextDataProvider.GetUserCode(HttpContext);
    }
}

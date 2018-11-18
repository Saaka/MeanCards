using MeanCards.ViewModel.Creation.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeanCards.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Authenticate([FromBody]RegisterUserViewModel model)
        {
            return Ok(model);
        }

        [AllowAnonymous]
        [HttpPost("google")]
        public IActionResult AuthorizeGoogle([FromBody]AuthorizeGoogleTokenViewModel model)
        {
            return Ok(model);
        }
    }
}

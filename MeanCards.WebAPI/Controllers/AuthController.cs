using MeanCards.ViewModel.Auth;
using MeanCards.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MeanCards.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        protected readonly IAuthenticateService authenticateService;

        public AuthController(IAuthenticateService authenticateService)
        {
            this.authenticateService = authenticateService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<AuthenticateUserResult>> Authenticate([FromBody]RegisterUserRequest model)
        {
            var result = await authenticateService.RegisterUser(model);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticateUserResult>> Login([FromBody]AuthenticateUserRequest model)
        {
            return Ok(model);
        }

        [AllowAnonymous]
        [HttpPost("google")]
        public async Task<ActionResult<AuthenticateUserResult>> AuthenticateGoogle([FromBody]AuthenticateUserWithGoogleRequest model)
        {
            return Ok(model);
        }
    }
}

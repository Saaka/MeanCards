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

            if (result.IsSuccessful)
                return Ok(result);
            else
                return BadRequest(result.Error);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticateUserResult>> Login([FromBody]AuthenticateUserRequest model)
        {
            var result = await authenticateService.AuthenticateUser(model);

            if (result.IsSuccessful)
                return Ok(result);
            else
                return BadRequest(result.Error);
        }

        [AllowAnonymous]
        [HttpPost("google")]
        public async Task<ActionResult<AuthenticateUserResult>> AuthenticateGoogle([FromBody]AuthenticateUserWithGoogleRequest model)
        {
            var result = await authenticateService.AuthenticateGoogleToken(model);

            if (result.IsSuccessful)
                return Ok(result);
            else
                return BadRequest(result.Error);
        }
    }
}

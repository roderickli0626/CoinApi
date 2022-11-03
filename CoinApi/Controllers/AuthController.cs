using CoinApi.Request_Models;
using CoinApi.Response_Models;
using CoinApi.Services.AuthService;
using CoinApi.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CoinApi.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            if (user is null)
                return BadRequest("Invalid client request");

            AuthenticatedResponse resp = authService.Login(user);

            return resp == null ? Unauthorized() : Ok(resp);
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterModel user)
        {
            if (user is null)
                return BadRequest("Invalid registration model");

            AuthenticatedResponse resp = authService.Register(user);

            return resp == null ? Unauthorized() : Ok(resp);
        }
    }
}

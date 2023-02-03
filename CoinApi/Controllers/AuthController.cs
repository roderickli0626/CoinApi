using CoinApi.Request_Models;
using CoinApi.Response_Models;
using CoinApi.Services.AuthService;
using CoinApi.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CoinApi.Shared;

namespace CoinApi.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService authService;
        private readonly IUserService userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            this.authService = authService;
            this.userService = userService;
        }
        [HttpPost("TestConnection")]
        public async Task<IActionResult> TestConnection([FromBody] LoginModel user)
        {
            return Unauthorized();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel user)
        {
            if (user is null)
                return BadRequest("Invalid client request");

            AuthenticatedResponse resp = await authService.Login(user);

            return resp == null ? Unauthorized() : Ok(resp);
        }
        [HttpPost("CheckLogin")]
        public async Task<IActionResult> CheckLogin([FromBody] LoginModel user)
        {
            return Ok(await authService.CheckLogin(user));
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel user)
        {
            if (user is null)
                return BadRequest("Invalid registration model");

            AuthenticatedResponse resp = await authService.Register(user);

            return resp == null ? Unauthorized() : Ok(resp);
        }
        [HttpPost("ForgotPasswordRequest")]
        public async Task<ActionResult<ApiResponse>> ForgotPasswordRequest([FromBody] string email)
        {
            return await authService.SendResetPasswordRequest(email);
        }
        [HttpPost("SendResetPasswordRequest")]
        public async Task<ActionResult<ApiResponse>> SendResetPasswordRequest(string email)
        {
            return await authService.SendResetPasswordRequest(email);
        }
        [HttpPost("UpdatePassword")]
        public async Task<ActionResult<ApiResponse>> UpdatePassword(ChangePasswordDto changePassword)
        {
            return await userService.UpdatePassword(changePassword);
        }
    }
}

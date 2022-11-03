using CoinApi.DB_Models;
using CoinApi.Services.LanguageService;
using CoinApi.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace CoinApi.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(userService.GetAll());
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int Id)
        {
            tblUser user = userService.GetById(Id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddLanguage([FromBody] tblUser model)
        {
            return Ok(userService.Create(model));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int Id)
        {
            tblUser user = userService.GetById(Id);

            if (user == null)
                return NotFound();

            userService.Delete(Id);
            return Ok();
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateLanguage([FromBody] tblUser model)
        {
            if (model is null)
                return NotFound();

            return Ok(userService.Update(model));
        }
    }
}

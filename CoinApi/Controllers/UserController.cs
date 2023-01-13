using CoinApi.DB_Models;
using CoinApi.Request_Models;
using CoinApi.Response_Models;
using CoinApi.Services.LanguageService;
using CoinApi.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        [HttpGet("GetUserInfoById")]
        public async Task<IActionResult> GetUserInfoById([FromQuery] int id)
        {
            return Ok(await userService.GetUserInfoById(id));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddUser([FromBody] tblUser model)
        {
            return Ok(await userService.CreateUser(model));
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

        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] tblUser model)
        {
            return Ok(await userService.UpdateUser(model));
        }
        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return Ok(await userService.DeleteUser(id));
        }
        [HttpGet("SetEnableLoginUser")]
        public async Task<IActionResult> SetEnableLoginUser(int id)
        {
            return Ok(await userService.SetEnableLogin(id));
        }
        [HttpPost("GetAllUsers")]
        public IActionResult GetAllUsers(bool isAll = false)
        {
            var data = new List<UserVM>();
            try
            {
                int draw = isAll ? 0 : Convert.ToInt32(Request.Form["draw"].FirstOrDefault());
                var start = isAll ? 0 : Convert.ToInt32(Request.Form["start"].FirstOrDefault());
                var length = isAll ? 10 : Convert.ToInt32(Request.Form["length"].FirstOrDefault());
                var sortColumn = isAll ? "" : Request.Form["order[0][column]"].FirstOrDefault();
                var sortColumnDirection = isAll ? "" : Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = isAll ? "" : Request.Form["search[value]"].FirstOrDefault();

                var obj = userService.GetAllUsers(searchValue, sortColumn, sortColumnDirection, start, length, isAll).Result;

                var output = JsonConvert.DeserializeObject<DataTableResponseVM>(obj.Data);
                data = JsonConvert.DeserializeObject<List<UserVM>>(output.Response);
                var modifiedData = data.Select(d =>
                    new
                    {
                        d.UserID,
                        d.FirstName,
                        d.SurName,
                        d.LastName,
                        d.Email,
                        d.Phone,
                        d.Adress,
                        d.CategoryName,
                        d.IsEnable
                    });
                var jsonData = new
                {
                    draw = draw,
                    recordsTotal = output.TotalRecords,
                    recordsFiltered = output.RecFilter,
                    error = string.Empty,
                    data = modifiedData,
                };
                return Json(jsonData);
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Message = ex.Message });
            }
        }
    }
}

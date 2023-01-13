using CoinApi.DB_Models;
using CoinApi.Request_Models;
using CoinApi.Response_Models;
using CoinApi.Services.ModuleService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoinApi.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IModuleService moduleService;

        public ModulesController(IModuleService moduleService)
        {
            this.moduleService = moduleService;
        }
        [HttpPost("AddUpdateModule")]
        public async Task<IActionResult> AddUpdateModule([FromBody] AddModuleVM model)
        {
            if (model.ModuleID == 0)
                return Ok(await moduleService.AddModule(model));

            else
                return Ok(await moduleService.UpdateModule(model));
        }

        [HttpGet("GetModuleInfoById")]
        public async Task<IActionResult> GetModuleInfoById(int id)
        {
            return Ok(await moduleService.GetModuleInfoById(id));
        }
        [HttpGet("GetGroupInfo")]
        public async Task<IActionResult> GetGroupInfo()
        {
            return Ok(await moduleService.GetGroupInfo());
        }
        [HttpGet("GetGroupDescriptionInfo")]
        public async Task<IActionResult> GetGroupDescriptionInfo()
        {
            return Ok(await moduleService.GetGroupDescriptionInfo());
        }
        [HttpPost("DeleteModule")]
        public async Task<IActionResult> DeleteModule(int id)
        {
            return Ok(await moduleService.DeleteModule(id));
        }
        [HttpPost("GetAllModules")]
        public IActionResult GetAllModules(bool isAll = false)
        {
            var data = new List<AllModuleDataVM>();
            try
            {
                int draw = isAll ? 0 : Convert.ToInt32(Request.Form["draw"].FirstOrDefault());
                var start = isAll ? 0 : Convert.ToInt32(Request.Form["start"].FirstOrDefault());
                var length = isAll ? 10 : Convert.ToInt32(Request.Form["length"].FirstOrDefault());
                var sortColumn = isAll ? "" : Request.Form["order[0][column]"].FirstOrDefault();
                var sortColumnDirection = isAll ? "" : Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = isAll ? "" : Request.Form["search[value]"].FirstOrDefault();
                var obj = moduleService.GetAllModules(searchValue, sortColumn, sortColumnDirection, start, length, isAll).Result;

                var output = JsonConvert.DeserializeObject<DataTableResponseVM>(obj.Data);
                data = JsonConvert.DeserializeObject<List<AllModuleDataVM>>(output.Response);
                var modifiedData = data.Select(d =>
                    new
                    {
                        d.ModuleID,
                        d.GroupNumberID,
                        d.NameModule,
                        d.Description,
                        d.Price,
                        d.ProductNumber,
                        d.File,
                        d.ModulePoints,
                        d.ModuleSubPoints,
                        d.SubscriptionDescription,
                        d.IsSubscription,
                        d.Color,
                        d.GroupDescription
                    });
                var jsonData = new
                {
                    draw = draw,
                    recordsTotal = output.TotalRecords,
                    recordsFiltered = output.RecFilter,
                    error = string.Empty,
                    data = modifiedData,
                };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                var jsonData = new { IsSuccess = false, Message = ex.Message };
                return Ok(jsonData);
            }
        }
    }
}

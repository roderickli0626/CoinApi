using CoinApi.DB_Models;
using CoinApi.Request_Models;
using CoinApi.Response_Models;
using CoinApi.Services;
using CoinApi.Services.LanguageService;
using CoinApi.Shared;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoinApi.Controllers
{
    public class LanguageController : BaseController
    {
        private readonly ILanguageService languageService;

        public LanguageController(ILanguageService languageService)
        {
            this.languageService = languageService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(languageService.GetAll());
        }
        [HttpGet("GetAllLanguages")]
        public async Task<IActionResult> GetAllLanguages()
        {
            return Ok(await languageService.GetAllLanguages());
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int Id)
        {
            tblLanguage language = languageService.GetById(Id);

            if (language == null)
                return NotFound();

            return Ok(language);
        }

        [HttpPost("AddUpdateLanguage")]
        public async Task<IActionResult> AddLanguage([FromBody] tblLanguage model)
        {
            if (model.languageNumber == 0)
            {
                return Ok(await languageService.AddLanguage(model));
            }
            else
            {
                return Ok(await languageService.UpdateLanguage(model));
            }

        }

        [HttpDelete("DeleteLanguage")]
        public async Task<IActionResult> DeleteLanguage([FromQuery] int Id)
        {
            return Ok(await languageService.DeleteLanguage(Id));
        }

        [HttpGet("GetLanguageInfoById")]
        public async Task<IActionResult> GetLanguageInfoById([FromQuery] int Id)
        {
            return Ok(await languageService.GetLanguageInfoById(Id));
        }
        [HttpPost("GetAllLanguages")]
        public IActionResult GetAllLanguages(bool isAll = false)
        {
            var data = new List<tblLanguage>();
            try
            {
                int draw = isAll ? 0 : Convert.ToInt32(Request.Form["draw"].FirstOrDefault());
                var start = isAll ? 0 : Convert.ToInt32(Request.Form["start"].FirstOrDefault());
                var length = isAll ? 10 : Convert.ToInt32(Request.Form["length"].FirstOrDefault());
                var sortColumn = isAll ? "" : Request.Form["order[0][column]"].FirstOrDefault();
                var sortColumnDirection = isAll ? "" : Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = isAll ? "" : Request.Form["search[value]"].FirstOrDefault();
                var obj = languageService.GetAllLanguages(searchValue, sortColumn, sortColumnDirection, start, length, isAll).Result;

                var output = JsonConvert.DeserializeObject<DataTableResponseVM>(obj.Data);
                data = JsonConvert.DeserializeObject<List<tblLanguage>>(output.Response);
                var modifiedData = data.Select(d =>
                    new
                    {
                        d.languageNumber,
                        d.description
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

        [HttpPost("loadDB")]
        public async Task<IActionResult> loadDB([FromBody] DbSyncRequest data)
        {
            var result = languageService.loadDB(data);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}

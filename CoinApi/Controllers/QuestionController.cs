using CoinApi.Request_Models;
using CoinApi.Response_Models;
using CoinApi.Services.LanguageGUIService;
using CoinApi.Services.QuestionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoinApi.Controllers
{
    public class QuestionController : BaseController
    {
        private readonly IQuestionService questionService;

        public QuestionController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }
        [HttpPost("AddUpdateQuestion")]
        public async Task<IActionResult> AddUpdateQuestion([FromBody] QuestionInfoVM questionInfo)
        {
            if (questionInfo.Id == 0)
                return Ok(await questionService.AddQuestion(questionInfo));

            else
                return Ok(await questionService.AddQuestion(questionInfo));
        }
        [HttpGet("GetQuestionInfoById")]
        public async Task<IActionResult> GetQuestionInfoById(string? search = "")
        {
            return Ok(await questionService.GetQuestionInfoById(search));
        }
        [HttpGet("GetQuestionInfoFromId")]
        public async Task<IActionResult> GetQuestionInfoFromId(int id)
        {
            return Ok(await questionService.GetQuestionInfoFromId(id));
        }
        [HttpPost("DeleteQuestion")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            return Ok(await questionService.DeleteQuestion(id));
        }
        [HttpPost("GetAllQuestions")]
        public IActionResult GetAllQuestions(bool isAll = false)
        {
            var data = new List<AllQuestionInfoVM>();
            try
            {
                int draw = isAll ? 0 : Convert.ToInt32(Request.Form["draw"].FirstOrDefault());
                var start = isAll ? 0 : Convert.ToInt32(Request.Form["start"].FirstOrDefault());
                var length = isAll ? 10 : Convert.ToInt32(Request.Form["length"].FirstOrDefault());
                var sortColumn = isAll ? "" : Request.Form["order[0][column]"].FirstOrDefault();
                var sortColumnDirection = isAll ? "" : Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = isAll ? "" : Request.Form["search[value]"].FirstOrDefault();
                var obj = questionService.GetAllQuestion(searchValue, sortColumn, sortColumnDirection, start, length, isAll).Result;

                var output = JsonConvert.DeserializeObject<DataTableResponseVM>(obj.Data);
                data = JsonConvert.DeserializeObject<List<AllQuestionInfoVM>>(output.Response);
                var modifiedData = data.Select(d =>
                    new
                    {
                        d.Id,
                        d.Title,
                        d.QuestionCount
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
            var result = questionService.loadDB(data);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}

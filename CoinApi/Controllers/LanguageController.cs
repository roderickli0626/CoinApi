using CoinApi.DB_Models;
using CoinApi.Services;
using CoinApi.Services.LanguageService;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int Id)
        {
            tblLanguage language = languageService.GetById(Id);

            if (language == null)
                return NotFound();

            return Ok(language);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddLanguage([FromBody] tblLanguage model)
        {
            return Ok(languageService.Create(model));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int Id)
        {
            tblLanguage language = languageService.GetById(Id);

            if (language == null)
                return NotFound();

            languageService.Delete(Id);
            return Ok();
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateLanguage([FromBody] tblLanguage model)
        {
            if (model is null)
                return NotFound();

            return languageService.Update(model) ? Ok() : NotFound();
        }
    }
}

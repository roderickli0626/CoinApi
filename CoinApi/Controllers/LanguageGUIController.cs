using CoinApi.DB_Models;
using CoinApi.Request_Models;
using CoinApi.Services.LanguageGUIService;
using CoinApi.Services.SubstanceService;
using Microsoft.AspNetCore.Mvc;

namespace CoinApi.Controllers
{
    public class LanguageGUIController : BaseController
    {
        private readonly ILanguageGUIService languageGUIService;

        public LanguageGUIController (ILanguageGUIService languageGUIService)
        {
            this.languageGUIService = languageGUIService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(languageGUIService.GetAll());
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int Id)
        {
            tblLanguageGUI substance = languageGUIService.GetById(Id);

            if (substance == null)
                return NotFound();

            return Ok(substance);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddLanguage([FromBody] tblLanguageGUI model)
        {
            return Ok(languageGUIService.Create(model));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int Id)
        {
            tblLanguageGUI substance = languageGUIService.GetById(Id);

            if (substance == null)
                return NotFound();

            languageGUIService.Delete(Id);
            return Ok();
        }

        [HttpPost("Update")]
        public IActionResult UpdateLanguage([FromBody] tblLanguageGUI model)
        {
            if (model is null)
                return BadRequest("Invalid Language Model");

            return languageGUIService.Update(model) ? Ok() : NotFound();
        }

        [HttpPost("loadDB")]
        public async Task<IActionResult> loadDB([FromBody] DbSyncRequest data)
        {
            var result = languageGUIService.loadDB(data);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}

using CoinApi.DB_Models;
using CoinApi.Request_Models;
using CoinApi.Services.SubstanceGroupService;
using CoinApi.Services.SubstanceGroupTextService;
using Microsoft.AspNetCore.Mvc;

namespace CoinApi.Controllers
{
    public class SubstanceGroupTextController : BaseController
    {
        private readonly ISubstanceGroupTextService substanceGroupTextService;

        public SubstanceGroupTextController(ISubstanceGroupTextService substanceGroupTextService)
        {
            this.substanceGroupTextService = substanceGroupTextService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(substanceGroupTextService.GetAll());
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int Id)
        {
            tblSubstanceGroupText substanceGroupText = substanceGroupTextService.GetById(Id);

            if (substanceGroupText == null)
                return NotFound();

            return Ok(substanceGroupText);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddLanguage([FromBody] tblSubstanceGroupText model)
        {
            return Ok(substanceGroupTextService.Create(model));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int Id)
        {
            tblSubstanceGroupText substanceGroupText = substanceGroupTextService.GetById(Id);

            if (substanceGroupText == null)
                return NotFound();

            substanceGroupTextService.Delete(Id);
            return Ok();
        }

        [HttpPost("Update")]
        public IActionResult UpdateLanguage([FromBody] tblSubstanceGroupText model)
        {
            if (model is null)
                return BadRequest("Invalid Language Model");

            return substanceGroupTextService.Update(model) ? Ok() : NotFound();
        }

        [HttpPost("loadDB")]
        public async Task<IActionResult> loadDB([FromBody] DbSyncRequest data)
        {
            var result = substanceGroupTextService.loadDB(data);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}

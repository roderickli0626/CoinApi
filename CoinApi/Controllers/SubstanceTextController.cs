using CoinApi.DB_Models;
using CoinApi.Request_Models;
using CoinApi.Services.SubstanceService;
using CoinApi.Services.SubstanceTextService;
using Microsoft.AspNetCore.Mvc;

namespace CoinApi.Controllers
{
    public class SubstanceTextController : BaseController
    {
        private readonly ISubstanceTextService substanceTextService;

        public SubstanceTextController(ISubstanceTextService substanceTextService)
        {
            this.substanceTextService = substanceTextService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(substanceTextService.GetAll());
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int Id)
        {
            tblSubstanceText substanceText = substanceTextService.GetById(Id);

            if (substanceText == null)
                return NotFound();

            return Ok(substanceText);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddLanguage([FromBody] tblSubstanceText model)
        {
            return Ok(substanceTextService.Create(model));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int Id)
        {
            tblSubstanceText substanceText = substanceTextService.GetById(Id);

            if (substanceText == null)
                return NotFound();

            substanceTextService.Delete(Id);
            return Ok();
        }

        [HttpPost("Update")]
        public IActionResult UpdateLanguage([FromBody] tblSubstanceText model)
        {
            if (model is null)
                return BadRequest("Invalid Language Model");

            return substanceTextService.Update(model) ? Ok() : NotFound();
        }
        [HttpGet("GetSubStanceByGroupId")]
        public async Task<IActionResult> GetSubStanceByGroupId([FromQuery] int Id)
        {
            return Ok(await substanceTextService.GetSubStanceByGroupId(Id));
        }

        [HttpPost("loadDB")]
        public async Task<IActionResult> loadDB([FromBody] DbSyncRequest data)
        {
            var result = substanceTextService.loadDB(data);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}

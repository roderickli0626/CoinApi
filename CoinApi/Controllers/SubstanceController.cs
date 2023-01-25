using CoinApi.DB_Models;
using CoinApi.Request_Models;
using CoinApi.Response_Models;
using CoinApi.Services.SubstanceGroupService;
using CoinApi.Services.SubstanceService;
using Microsoft.AspNetCore.Mvc;

namespace CoinApi.Controllers
{
    public class SubstanceController : BaseController
    {
        private readonly ISubstanceService substanceService;

        public SubstanceController(ISubstanceService substanceService)
        {
            this.substanceService = substanceService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(substanceService.GetAll());
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int Id)
        {
            tblSubstance substance = substanceService.GetById(Id);

            if (substance == null)
                return NotFound();

            return Ok(substance);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddLanguage([FromBody] tblSubstance model)
        {
            return Ok(substanceService.Create(model));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int Id)
        {
            tblSubstance substance = substanceService.GetById(Id);

            if (substance == null)
                return NotFound();

            substanceService.Delete(Id);
            return Ok();
        }

        [HttpPost("Update")]
        public IActionResult UpdateLanguage([FromBody] tblSubstance model)
        {
            if (model is null)
                return BadRequest("Invalid Language Model");

            return substanceService.Update(model) ? Ok() : NotFound();
        }

        [HttpPost("loadDB")]
        public async Task<IActionResult> loadDB([FromBody] DbSyncRequest data)
        {
            var result =  substanceService.loadDB(data);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("loadAllDB")]
        public async Task<IActionResult> loadAllDB([FromBody] DbSyncRequest data)
        {
            var result = substanceService.loadAllDB(data);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}

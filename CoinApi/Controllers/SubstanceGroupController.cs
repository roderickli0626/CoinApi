using CoinApi.DB_Models;
using CoinApi.Services.SubstanceForGroupService;
using CoinApi.Services.SubstanceGroupService;
using Microsoft.AspNetCore.Mvc;

namespace CoinApi.Controllers
{
    public class SubstanceGroupController : BaseController
    {
        private readonly ISubstanceGroupService substanceGroupService;

        public SubstanceGroupController(ISubstanceGroupService substanceGroupService)
        {
            this.substanceGroupService = substanceGroupService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(substanceGroupService.GetAll());
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int Id)
        {
            tblSubstanceGroup substanceGroup = substanceGroupService.GetById(Id);

            if (substanceGroup == null)
                return NotFound();

            return Ok(substanceGroup);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddLanguage([FromBody] tblSubstanceGroup model)
        {
            return Ok(substanceGroupService.Create(model));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int Id)
        {
            tblSubstanceGroup substanceGroup = substanceGroupService.GetById(Id);

            if (substanceGroup == null)
                return NotFound();

            substanceGroupService.Delete(Id);
            return Ok();
        }

        [HttpPost("Update")]
        public IActionResult UpdateLanguage([FromBody] tblSubstanceGroup model)
        {
            if (model is null)
                return BadRequest("Invalid Language Model");

            return substanceGroupService.Update(model) ? Ok() : NotFound();
        }
    }
}

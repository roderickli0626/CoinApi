using CoinApi.DB_Models;
using CoinApi.Services.SubstanceForGroupService;
using CoinApi.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace CoinApi.Controllers
{
    public class SubstanceForGroupController : BaseController
    {
        private readonly ISubstanceForGroupService substanceForGroupService;

        public SubstanceForGroupController(ISubstanceForGroupService substanceForGroupService)
        {
            this.substanceForGroupService = substanceForGroupService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(substanceForGroupService.GetAll());
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int Id)
        {
            tblSubstanceForGroup substanceForGroup = substanceForGroupService.GetById(Id);

            if (substanceForGroup == null)
                return NotFound();

            return Ok(substanceForGroup);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddLanguage([FromBody] tblSubstanceForGroup model)
        {
            return Ok(substanceForGroupService.Create(model));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int Id)
        {
            tblSubstanceForGroup substanceForGroup = substanceForGroupService.GetById(Id);

            if (substanceForGroup == null)
                return NotFound();

            substanceForGroupService.Delete(Id);
            return Ok();
        }

        [HttpPost("Update")]
        public IActionResult UpdateLanguage([FromBody] tblSubstanceForGroup model)
        {
            if (model is null)
                return BadRequest("Invalid SubstanceForGroup Model");

            return substanceForGroupService.Update(model) ? Ok() : NotFound();
        }
    }
}

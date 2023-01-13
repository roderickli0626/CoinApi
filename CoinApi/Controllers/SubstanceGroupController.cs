using CoinApi.DB_Models;
using CoinApi.Request_Models;
using CoinApi.Response_Models;
using CoinApi.Services.SubstanceForGroupService;
using CoinApi.Services.SubstanceGroupService;
using CoinApi.Services.SubstanceService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoinApi.Controllers
{
    public class SubstanceGroupController : BaseController
    {
        private readonly ISubstanceGroupService substanceGroupService;
        private readonly ISubstanceService substanceService;

        public SubstanceGroupController(ISubstanceGroupService substanceGroupService, ISubstanceService substanceService)
        {
            this.substanceGroupService = substanceGroupService;
            this.substanceService = substanceService;
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

        [HttpPost("AddUpdateGroup")]
        public async Task<IActionResult> AddUpdateGroup([FromBody] GroupInfoDto groupInfo)
        {
            if (groupInfo.Id == 0)
            {
                return Ok(await substanceGroupService.AddGroup(groupInfo));
            }
            else
            {
                return Ok(await substanceGroupService.UpdateGroup(groupInfo));
            }

        }
        [HttpGet("GetGroupInfoById")]
        public async Task<IActionResult> GetGroupInfoById(int id)
        {
            return Ok(await substanceGroupService.GetGroupInfoById(id));
        }
        [HttpPost("DeleteGroup")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            return Ok(await substanceGroupService.DeleteGroup(id));
        }
        [HttpPost("GetAllGroups")]
        public IActionResult GetAllGroups(bool isAll = false)
        {
            var data = new List<GroupInfoDto>();
            try
            {
                int draw = isAll ? 0 : Convert.ToInt32(Request.Form["draw"].FirstOrDefault());
                var start = isAll ? 0 : Convert.ToInt32(Request.Form["start"].FirstOrDefault());
                var length = isAll ? 10 : Convert.ToInt32(Request.Form["length"].FirstOrDefault());
                var sortColumn = isAll ? "" : Request.Form["order[0][column]"].FirstOrDefault();
                var sortColumnDirection = isAll ? "" : Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = isAll ? "" : Request.Form["search[value]"].FirstOrDefault();

                var obj = substanceGroupService.GetAllGroups(searchValue, sortColumn, sortColumnDirection, start, length, isAll).Result;

                var output = JsonConvert.DeserializeObject<DataTableResponseVM>(obj.Data);
                data = JsonConvert.DeserializeObject<List<GroupInfoDto>>(output.Response);
                var modifiedData = data.Select(d =>
                    new
                    {
                        d.Id,
                        d.GroupName,
                        d.Language,
                        d.IsHide,
                        d.IsStandard,
                    });
                var jsonData = new
                {
                    draw = draw,
                    recordsTotal = output.TotalRecords,
                    recordsFiltered = output.RecFilter,
                    error = string.Empty,
                    data = modifiedData,
                };
                return Json(jsonData);
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Message = ex.Message });
            }
        }


        [HttpPost("AddUpdateSubStanceGroup")]
        public async Task<IActionResult> AddUpdateSubStanceGroup([FromBody] SubStanceGroupInfoVM subStanceGroupInfoVM)
        {
            if (subStanceGroupInfoVM.Id == 0)
            {
                return Ok(await substanceService.AddSubStanceGroup(subStanceGroupInfoVM));
            }
            else
            {
                return Ok(await substanceService.UpdateSubStanceGroup(subStanceGroupInfoVM));
            }

        }
        [HttpGet("GetSubStanceGroupInfoById")]
        public async Task<IActionResult> GetSubStanceGroupInfoById(int id)
        {
            return Ok(await substanceService.GetSubStanceGroupInfoById(id));
        }
        [HttpPost("DeleteSubStanceGroup")]
        public async Task<IActionResult> DeleteSubStanceGroup(int id)
        {
            return Ok(await substanceService.DeleteSubStanceGroup(id));
        }
        [HttpPost("AddImportGroupSubStance")]
        public async Task<IActionResult> AddImportGroupSubStance([FromForm] IFormFileCollection file, bool isGroup)
        {
            return Ok(await substanceService.AddImportGroupSubStance(file, isGroup));
        }

        [HttpPost("GetAllSubStanceGroups")]
        public IActionResult GetAllSubStanceGroups(bool isAll = false)
        {
            var data = new List<SubStanceGroupInfoVM>();
            try
            {
                int draw = isAll ? 0 : Convert.ToInt32(Request.Form["draw"].FirstOrDefault());
                var start = isAll ? 0 : Convert.ToInt32(Request.Form["start"].FirstOrDefault());
                var length = isAll ? 10 : Convert.ToInt32(Request.Form["length"].FirstOrDefault());
                var sortColumn = isAll ? "" : Request.Form["order[0][column]"].FirstOrDefault();
                var sortColumnDirection = isAll ? "" : Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = isAll ? "" : Request.Form["search[value]"].FirstOrDefault();

                var obj = substanceService.GetAllSubstanceGroups(searchValue, sortColumn, sortColumnDirection, start, length, isAll).Result;

                var output = JsonConvert.DeserializeObject<DataTableResponseVM>(obj.Data);
                data = JsonConvert.DeserializeObject<List<SubStanceGroupInfoVM>>(output.Response);
                var modifiedData = data.Select(d =>
                    new
                    {
                        d.Id,
                        d.SubStanceName,
                        d.Language,
                        d.IsHide,
                        d.IsStandard,
                        d.Duration,
                        DateCreate = d.DateCreate != null ? d.DateCreate.Value.ToString("MM/dd/yyyy") : ""
                    });
                var jsonData = new
                {
                    draw = draw,
                    recordsTotal = output.TotalRecords,
                    recordsFiltered = output.RecFilter,
                    error = string.Empty,
                    data = modifiedData,
                };
                return Json(jsonData);
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Message = ex.Message });
            }
        }
    }
}

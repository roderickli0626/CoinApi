using CoinApi.Request_Models;
using CoinApi.Response_Models;
using CoinApi.Services.MasterService;
using CoinApi.Services.ModuleService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoinApi.Controllers
{
    //[ApiController]
    public class MasterController : BaseController
    {
        private readonly IMasterService _masterService;

        public MasterController(IMasterService masterService)
        {
            _masterService = masterService;
        }

        [HttpGet("GetAllCategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            return new OkObjectResult(await _masterService.GetAllCategory());
        }
        [HttpGet("GetAllCountry")]
        public async Task<IActionResult> GetAllCountry()
        {
            return new OkObjectResult(await _masterService.GetAllCountry()); ;
        }

        #region Coupon
        [HttpPost("GetAllCoupons")]
        public IActionResult GetAllCoupons(bool isAll = false)
        {
            var data = new List<CouponDto>();
            try
            {
                int draw = isAll ? 0 : Convert.ToInt32(Request.Form["draw"].FirstOrDefault());
                var start = isAll ? 0 : Convert.ToInt32(Request.Form["start"].FirstOrDefault());
                var length = isAll ? 10 : Convert.ToInt32(Request.Form["length"].FirstOrDefault());
                var sortColumn = isAll ? "" : Request.Form["order[0][column]"].FirstOrDefault();
                var sortColumnDirection = isAll ? "" : Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = isAll ? "" : Request.Form["search[value]"].FirstOrDefault();
                var obj = _masterService.GetAllCoupons(searchValue, sortColumn, sortColumnDirection, start, length, isAll).Result;

                var output = JsonConvert.DeserializeObject<DataTableResponseVM>(obj.Data);
                data = JsonConvert.DeserializeObject<List<CouponDto>>(output.Response);
                var modifiedData = data.Select(d =>
                    new
                    {
                        d.Id,
                        d.CoupenCode,
                        d.DiscountPercentage,
                        d.DiscountAmount,
                        d.MinAmount,
                        Date = d.Date != null ? d.Date.Value.ToString("MM/dd/yyyy") : ""
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
        [HttpPost("AddUpdateCoupon")]
        public async Task<IActionResult> AddUpdateModule( CouponDto model)
        {
            if (model.Id == 0)
            {
                return Ok(await _masterService.AddCoupon(model));
            }
            else
            {
                return Ok(await _masterService.UpdateCoupon(model));
            }

        }
        [HttpGet("GetCouponInfoById")]
        public async Task<IActionResult> GetCouponInfoById(int id)
        {
            return Ok(await _masterService.GetCouponInfoById(id));
        }
        [HttpGet("GetCouponInfoByCode")]
        public async Task<IActionResult> GetCouponInfoByCode(string code)
        {
            return Ok(await _masterService.GetCouponInfoByCode(code));
        }
        [HttpPost("DeleteCoupon")]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            return Ok(await _masterService.DeleteCoupon(id));
        }
        #endregion
    }
}

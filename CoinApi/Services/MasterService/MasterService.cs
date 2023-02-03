using CoinApi.Context;
using CoinApi.DB_Models;
using CoinApi.Enums;
using CoinApi.Request_Models;
using CoinApi.Response_Models;
using CoinApi.Shared;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static CoinApi.Shared.ApiFunctions;
using static Dapper.SqlMapper;

namespace CoinApi.Services.MasterService
{
    public class MasterService : IMasterService
    {
        protected readonly CoinApiContext context;

        public MasterService(CoinApiContext context)
        {
            this.context = context;
        }
        public async Task<ApiResponse> GetAllCategory()
        {
            return ApiSuccessResponse(await context.tblCategory.OrderBy(s => s.OrderNo).ToListAsync());
        }
        public async Task<ApiResponse> GetAllCountry()
        {
            return ApiSuccessResponse(await context.tblCountry.ToListAsync());
        }
        #region Coupon
        public async Task<ApiResponse> GetAllCoupons(string search = null, string order = "0", string orderDir = "asc", int startRec = 0, int pageSize = 10, bool isAll = false)
        {
            var data = new List<CouponDto>();
            try
            {

                data = await (from tm in context.tblCoupons
                              where tm.IsActive == true
                              select new CouponDto
                              {
                                  Id = tm.Id,
                                  CoupenCode = tm.CoupenCode,
                                  DiscountAmount = tm.DiscountAmount,
                                  DiscountPercentage = tm.DiscountPercentage,
                                  MinAmount = tm.MinAmount,
                                  Date = tm.CreatedDatetime,

                              }).ToListAsync();



                int totalRecords = data.Count;
                if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
                {
                    data = data.Where(p => (p.CoupenCode != null && p.CoupenCode.ToString().ToLower().Contains(search.ToLower())) ||
                    (p.DiscountAmount != null && p.DiscountAmount.ToString().ToLower().Contains(search.ToLower())) ||
                    (p.DiscountPercentage != null && p.DiscountPercentage.ToString().ToLower().Contains(search.ToLower())) ||
                    (p.Date != null && p.Date.ToString().ToLower().Contains(search.ToLower()))).ToList();
                }
                data = SortTableCouponList(order, orderDir, data);
                int recFilter = data.Count;
                data = isAll ? data.ToList() : data.Skip(startRec).Take(pageSize).ToList();
                DataTableResponseVM model = new DataTableResponseVM
                {
                    RecFilter = recFilter,
                    TotalRecords = totalRecords,
                    Response = JsonConvert.SerializeObject(data)
                };
                return new ApiResponse
                {
                    IsSuccess = true,
                    Data = JsonConvert.SerializeObject(model)
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        private List<CouponDto> SortTableCouponList(string order, string orderDir, List<CouponDto> data)
        {
            List<CouponDto> stateList = new List<CouponDto>();
            try
            {
                switch (order)
                {
                    case "0":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Date).ToList() : data.OrderBy(p => p.Date).ToList();
                        break;
                    case "1":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.CoupenCode).ToList() : data.OrderBy(p => p.CoupenCode).ToList();
                        break;
                    case "2":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.DiscountAmount).ToList() : data.OrderBy(p => p.DiscountAmount).ToList();
                        break;
                    case "3":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.DiscountPercentage).ToList() : data.OrderBy(p => p.DiscountPercentage).ToList();
                        break;
                    default:
                        stateList = data.OrderByDescending(p => p.Id).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return stateList;
        }
        public async Task<ApiResponse> AddCoupon(CouponDto info)
        {
            try
            {
                if (info == null)
                    return ApiErrorResponse("Coupon not found.");

                var chkExist = context.tblCoupons.FirstOrDefault(s => s.CoupenCode.Trim().ToLower() == info.CoupenCode.Trim().ToLower());
                if (chkExist != null)
                    return ApiErrorResponse("Please enter unique coupon code.");


                tblCoupons tblCoupons = new tblCoupons()
                {
                    CoupenCode = info.CoupenCode,
                    DiscountAmount = info.DiscountAmount,
                    MinAmount = info.MinAmount,
                    DiscountPercentage = info.DiscountPercentage,
                    IsActive = true,
                    CreatedDatetime = DateTime.UtcNow,
                    IsAmount = info.DiscountAmount != 0 ? true : false
                };
                await context.tblCoupons.AddAsync(tblCoupons);
                await context.SaveChangesAsync();
                return ApiSuccessResponses(null, "Coupon create successfully.");

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<ApiResponse> UpdateCoupon(CouponDto info)
        {
            try
            {
                if (info == null)
                    return ApiErrorResponse("Coupon not found.");

                tblCoupons? couponInfo = context.tblCoupons.FirstOrDefault(x => x.Id == info.Id);
                if (couponInfo == null)
                    return ApiErrorResponse("Coupon not found.");

                var chkExist = context.tblCoupons.FirstOrDefault(s => s.CoupenCode.Trim().ToLower() == info.CoupenCode.Trim().ToLower() && s.Id != info.Id);
                if (chkExist != null)
                    return ApiErrorResponse("Please enter unique coupon code.");


                couponInfo.CoupenCode = info.CoupenCode;
                couponInfo.DiscountAmount = info.DiscountAmount;
                couponInfo.DiscountPercentage = info.DiscountPercentage;
                couponInfo.MinAmount = info.MinAmount;
                couponInfo.UpdatedDatetime = DateTime.UtcNow;
                couponInfo.IsAmount = info.DiscountAmount != 0 ? true : false;
                await context.SaveChangesAsync();
                return ApiSuccessResponses(null, "Coupon update successfully.");

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<ApiResponse> GetCouponInfoById(int id)
        {

            var info = await context.tblCoupons.FirstOrDefaultAsync(s => s.Id == id);
            if (info == null)
                return ApiErrorResponse("Please enter valid coupon.");

            return ApiSuccessResponse(info);
        }
        public async Task<ApiResponse> GetCouponInfoByCode(string code)
        {

            var info = await context.tblCoupons.FirstOrDefaultAsync(s => s.CoupenCode == code);
            if (info == null)
                return ApiErrorResponse("Please enter valid coupon code.");

            return ApiSuccessResponse(info);
        }
        public async Task<ApiResponse> DeleteCoupon(int id)
        {
            var getInfo = await context.tblCoupons.FirstOrDefaultAsync(s => s.Id == id);
            if (getInfo == null)
                return ApiErrorResponse("Please enter valid coupon.");

            context.tblCoupons.Remove(getInfo);
            await context.SaveChangesAsync();
            return ApiSuccessResponses(null, "Coupon successfully deleted.");
        }
        #endregion



        #region PayPal
        public async Task<ApiResponse> GetAllPayPal(string search = null, string order = "0", string orderDir = "asc", int startRec = 0, int pageSize = 10, bool isAll = false)
        {
            var data = new List<tblPayPalDto>();
            try
            {

                data = await (from tm in context.tblPayPalConfiguration
                              select new tblPayPalDto
                              {
                                  Id = tm.Id,
                                  ClientId = tm.ClientId,
                                  ClientSecret = tm.ClientSecret,
                                  Date = tm.CreatedDatetime,
                              }).ToListAsync();



                int totalRecords = data.Count;
                if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
                {
                    data = data.Where(p => (p.ClientId != null && p.ClientId.ToString().ToLower().Contains(search.ToLower())) ||
                    (p.ClientSecret != null && p.ClientSecret.ToString().ToLower().Contains(search.ToLower())) ||
                    (p.Date != null && p.Date.ToString().ToLower().Contains(search.ToLower()))).ToList();
                }
                data = SortTablePayPalList(order, orderDir, data);
                int recFilter = data.Count;
                data = isAll ? data.ToList() : data.Skip(startRec).Take(pageSize).ToList();
                DataTableResponseVM model = new DataTableResponseVM
                {
                    RecFilter = recFilter,
                    TotalRecords = totalRecords,
                    Response = JsonConvert.SerializeObject(data)
                };
                return new ApiResponse
                {
                    IsSuccess = true,
                    Data = JsonConvert.SerializeObject(model)
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        private List<tblPayPalDto> SortTablePayPalList(string order, string orderDir, List<tblPayPalDto> data)
        {
            List<tblPayPalDto> stateList = new List<tblPayPalDto>();
            try
            {
                switch (order)
                {
                    case "0":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Date).ToList() : data.OrderBy(p => p.Date).ToList();
                        break;
                    case "1":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.ClientId).ToList() : data.OrderBy(p => p.ClientId).ToList();
                        break;
                    case "2":
                        stateList = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.ClientSecret).ToList() : data.OrderBy(p => p.ClientSecret).ToList();
                        break;

                    default:
                        stateList = data.OrderByDescending(p => p.Id).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return stateList;
        }
        public async Task<ApiResponse> AddPayPal(tblPayPalDto info)
        {
            try
            {
                if (info == null)
                    return ApiErrorResponse("Paypal configuration not found.");

                var chkExist = context.tblPayPalConfiguration.FirstOrDefault();
                if (chkExist == null)
                {
                    tblPayPalConfiguration tblConfiguratINfo = new tblPayPalConfiguration()
                    {
                        ClientId = info.ClientId,
                        ClientSecret = info.ClientSecret,
                        CreatedDatetime = DateTime.UtcNow,
                        Mode = "Sandbox"
                    };
                    await context.tblPayPalConfiguration.AddAsync(tblConfiguratINfo);
                    await context.SaveChangesAsync();
                    return ApiSuccessResponses(null, "PayPal configuration create successfully.");
                }
                else
                {
                    chkExist.ClientId = info.ClientId;
                    chkExist.ClientSecret = info.ClientSecret;
                    chkExist.UpdatedDatetime = DateTime.UtcNow;
                    await context.SaveChangesAsync();

                }
                return ApiSuccessResponses(null, "PayPal configuration update successfully.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ApiResponse> GetPayPalInfoById(int id)
        {

            var info = await context.tblPayPalConfiguration.FirstOrDefaultAsync(s => s.Id == id);
            if (info == null)
                return ApiErrorResponse("Please enter valid paypal configuration.");

            return ApiSuccessResponse(info);
        }

        public async Task<ApiResponse> DeletePayPal(int id)
        {
            var getInfo = await context.tblPayPalConfiguration.FirstOrDefaultAsync(s => s.Id == id);
            if (getInfo == null)
                return ApiErrorResponse("Please enter valid paypal configuration.");

            context.tblPayPalConfiguration.Remove(getInfo);
            await context.SaveChangesAsync();
            return ApiSuccessResponses(null, "Paypal successfully deleted.");
        }
        #endregion
    }
}

using CoinApi.DB_Models;
using CoinApi.Request_Models;
using CoinApi.Shared;

namespace CoinApi.Services.MasterService
{
    public interface IMasterService
    {
        Task<ApiResponse> GetAllCategory();
        Task<ApiResponse> GetAllCountry();
        #region Coupon
        Task<ApiResponse> AddCoupon(CouponDto couponDto);
        Task<ApiResponse> UpdateCoupon(CouponDto couponDto);
        Task<ApiResponse> GetCouponInfoById(int id);
        Task<ApiResponse> GetCouponInfoByCode(string code);
        Task<ApiResponse> GetAllCoupons(string search, string order, string orderDir, int startRec, int pageSize, bool isAll);
        Task<ApiResponse> DeleteCoupon(int id);
        #endregion

        #region PayPal
        Task<ApiResponse> AddPayPal(tblPayPalDto couponDto);

        Task<ApiResponse> GetPayPalInfoById(int id);

        Task<ApiResponse> GetAllPayPal(string search, string order, string orderDir, int startRec, int pageSize, bool isAll);
        Task<ApiResponse> DeletePayPal(int id);
        #endregion
    }
}

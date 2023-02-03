using CoinApi.Request_Models;
using CoinApi.Shared;

namespace CoinApi.Services.OrderService
{
    public interface IOrderService
    {
        Task<ApiResponse> CreateOrder(OrderInfoDto orderInfoDto);
        Task<ApiResponse> GetOrders(int id, string startDate, string toDate, bool isAdmin, int? searchUserId);
        Task<ApiResponse> GetOrderInfoById(int id);
        Task<ApiResponse> DeleteOrderById(int id);
    }

}
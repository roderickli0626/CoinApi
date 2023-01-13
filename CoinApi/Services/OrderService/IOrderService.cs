using CoinApi.Request_Models;
using CoinApi.Shared;

namespace CoinApi.Services.OrderService
{
    public interface IOrderService
    {
        Task<ApiResponse> CreateOrder(OrderInfoDto orderInfoDto);
    }
}

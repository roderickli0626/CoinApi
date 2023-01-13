using CoinApi.Context;
using CoinApi.DB_Models;
using CoinApi.Enums;
using CoinApi.Request_Models;
using CoinApi.Services.FileStorageService;
using CoinApi.Shared;
using Newtonsoft.Json;
using static CoinApi.Shared.ApiFunctions;

namespace CoinApi.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly CoinApiContext context;
        private readonly IFileStorageService _fileStorageService;

        public OrderService(CoinApiContext context, IFileStorageService fileStorageService)
        {
            this.context = context;
            _fileStorageService = fileStorageService;
        }
        public async Task<ApiResponse> CreateOrder(OrderInfoDto orderInfoDto)
        {
            try
            {
                if (orderInfoDto == null)
                    return ApiErrorResponse("Order not found.");




                tblOrders tblOrder = new tblOrders()
                {
                    ModuleID = orderInfoDto.ModuleId,
                    UserId = orderInfoDto.UserId,
                    Date = DateTime.UtcNow,
                    Amount = orderInfoDto.Amount,
                    DiscountAmount = orderInfoDto.DiscountAmount,
                    TotalAmount = orderInfoDto.TotalAmount,
                };
                context.tblOrders.Add(tblOrder);
                await context.SaveChangesAsync();

                return ApiSuccessResponses(null, "Order create successfully.");
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}

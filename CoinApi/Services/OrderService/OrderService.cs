using CoinApi.Context;
using CoinApi.DB_Models;
using CoinApi.Enums;
using CoinApi.Request_Models;
using CoinApi.Services.FileStorageService;
using CoinApi.Shared;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;
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

                foreach (var item in orderInfoDto.orderItemInfo)
                {
                    tblOrderItems tblOrderItems = new tblOrderItems()
                    {
                        ModuleId = item.ModuleId,
                        Qty = item.Qty,
                        Price = item.Price,
                        TotalPrice = item.TotalPrice,
                        OrderId = tblOrder.Id
                    };
                    context.tblOrderItems.AddRange(tblOrderItems);
                    await context.SaveChangesAsync();
                }

                return ApiSuccessResponses(null, "Order create successfully.");
            }
            catch (Exception ex)
            {

                return ApiValidationResponse(ex.Message);
            }

        }
        public async Task<ApiResponse> GetOrders(int id, string startDate, string endDate, bool isAdmin, int? searchUserId)
        {
            try
            {
                DateTime? fromDate = string.IsNullOrEmpty(startDate) ? (DateTime?)null : (DateTime.ParseExact(startDate, new string[] { "MM/dd/yyyy", "yyyy-MM-dd" }, CultureInfo.InvariantCulture, DateTimeStyles.None).Date);
                DateTime? toDate = string.IsNullOrEmpty(endDate) ? (DateTime?)null : (DateTime.ParseExact(endDate, new string[] { "MM/dd/yyyy", "yyyy-MM-dd" }, CultureInfo.InvariantCulture, DateTimeStyles.None).Date);
                List<OrderInfoDto> orderInfoDto = new List<OrderInfoDto>();
                var getOrders = await (from to in context.tblOrders
                                       join tu in context.tblUser on to.UserId equals tu.UserID into User
                                       from tu in User.DefaultIfEmpty()
                                       select new OrderInfoDto
                                       {
                                           Id = to.Id,
                                           UserId = to.UserId,
                                           Date = to.Date,
                                           Amount = to.Amount,
                                           DiscountAmount = to.DiscountAmount,
                                           TotalAmount = to.TotalAmount,
                                           UserName = tu.FirstName + " " + tu.LastName
                                       }).ToListAsync();
                if (isAdmin)
                {
                    getOrders = getOrders.ToList();
                    if (searchUserId != 0 && searchUserId != null)
                    {
                        getOrders = getOrders.Where(s => s.UserId == searchUserId).ToList();
                    }
                }
                else
                {
                    getOrders = getOrders.Where(s => s.UserId == id).ToList();
                }
                getOrders = getOrders.DistinctBy(s => s.Id).ToList();
                if (!string.IsNullOrEmpty(startDate))
                {
                    getOrders = getOrders.Where(s => s.Date != null && s.Date.Value.Date >= fromDate.Value.Date).ToList();
                }
                if (!string.IsNullOrEmpty(endDate))
                {
                    getOrders = getOrders.Where(s => s.Date != null && s.Date.Value.Date <= toDate.Value.Date).ToList();
                }
                foreach (var order in getOrders)
                {
                    var items = context.tblOrderItems.Where(s => s.OrderId == order.Id).Select(s => new OrderItemInfoDto
                    {
                        ModuleId = s.ModuleId,
                        Qty = s.Qty,
                        Price = s.Price,
                        TotalPrice = s.TotalPrice,
                        OrderId = s.OrderId
                    }).ToList();
                    order.orderItemInfo = items;
                }
                return ApiSuccessResponses(getOrders, "Get orders successfully.");
            }
            catch (Exception ex)
            {
                return ApiValidationResponse(ex.Message);
            }
        }


        public async Task<ApiResponse> GetOrderInfoById(int id)
        {
            try
            {
                OrderInfoDto orderInfoDto = new OrderInfoDto();
                var getOrders = await (from to in context.tblOrders
                                       where to.Id == id
                                       select new OrderInfoDto
                                       {
                                           Id = to.Id,
                                           UserId = to.UserId,
                                           Date = to.Date,
                                           Amount = to.Amount,
                                           DiscountAmount = to.DiscountAmount,
                                           TotalAmount = to.TotalAmount,
                                       }).FirstOrDefaultAsync();
                if (getOrders != null)
                {
                    getOrders.orderItemInfo = (from oi in context.tblOrderItems
                                               join tm in context.tblModules on oi.ModuleId equals tm.ModuleID
                                               where oi.OrderId == getOrders.Id
                                               select new OrderItemInfoDto
                                               {
                                                   ModuleId = oi.ModuleId,
                                                   Qty = oi.Qty,
                                                   Price = oi.Price,
                                                   TotalPrice = oi.TotalPrice,
                                                   OrderId = oi.OrderId,
                                                   ModuleName = tm.NameModule

                                               }).ToList();
                }

                return ApiSuccessResponses(getOrders, "Get orders successfully.");
            }
            catch (Exception ex)
            {
                return ApiValidationResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteOrderById(int id)
        {
            try
            {
                OrderInfoDto orderInfoDto = new OrderInfoDto();
                var getOrders = await context.tblOrders.Where(s => s.Id == id).FirstOrDefaultAsync();
                if (getOrders == null)
                {
                    return ApiValidationResponse("Order not found");
                }
                var orderItems = context.tblOrderItems.Where(s => s.OrderId == id).ToList();
                if (orderItems.Count != 0)
                {
                    context.tblOrderItems.RemoveRange(orderItems);
                    await context.SaveChangesAsync();
                }
                context.tblOrders.Remove(getOrders);
                await context.SaveChangesAsync();
                return ApiSuccessResponses(null, "Delete order successfully.");
            }
            catch (Exception ex)
            {
                return ApiValidationResponse(ex.Message);
            }
        }
    }
}

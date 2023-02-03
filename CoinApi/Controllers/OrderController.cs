using CoinApi.Request_Models;
using CoinApi.Services.ModuleService;
using CoinApi.Services.OrderService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoinApi.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderInfoDto model)
        {
            return Ok(await orderService.CreateOrder(model));
        }
        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders(int id, string startDate, string toDate, bool isAdmin = false, int? searchUserId = null)
        {
            startDate = startDate == "null" ? "" : startDate;
            toDate = toDate == "null" ? "" : toDate;
            return Ok(await orderService.GetOrders(id, startDate, toDate, isAdmin, searchUserId));
        }
        [HttpGet("GetOrderInfoById")]
        public async Task<IActionResult> GetOrderInfoById(int id)
        {
            return Ok(await orderService.GetOrderInfoById(id));
        }
        [HttpGet("DeleteOrderById")]
        public async Task<IActionResult> DeleteOrderById(int id)
        {
            return Ok(await orderService.DeleteOrderById(id));
        }
    }
}

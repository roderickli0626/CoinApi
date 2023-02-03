namespace CoinApi.Request_Models
{
    public class OrderInfoDto
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public int ModuleId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? Date { get; set; }
        public string UserName { get; set; }
        public List<OrderItemInfoDto> orderItemInfo { get; set; }
    }
    public class OrderItemInfoDto
    {
        public int? ModuleId { get; set; }
        public int? Qty { get; set; }
        public decimal? Price { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? OrderId { get; set; }
        public string ModuleName { get; set; }
    }
}

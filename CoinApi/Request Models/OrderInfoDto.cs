namespace CoinApi.Request_Models
{
    public class OrderInfoDto
    {
        public int? UserId { get; set; }
        public int ModuleId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}

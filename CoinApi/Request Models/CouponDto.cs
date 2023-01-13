namespace CoinApi.Request_Models
{
    public class CouponDto
    {
        public int Id { get; set; }
        public string CoupenCode { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public Nullable<decimal> MinAmount { get; set; }
        public DateTime? Date { get; set; }
    }
}

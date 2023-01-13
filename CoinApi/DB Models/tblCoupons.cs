using System.ComponentModel.DataAnnotations;

namespace CoinApi.DB_Models
{
    public class tblCoupons
    {
        [Key]
        public int Id { get; set; }
        public string CoupenCode { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public Nullable<decimal> MinAmount { get; set; }
        public Nullable<System.DateTime> CreatedDatetime { get; set; }
        public Nullable<System.DateTime> UpdatedDatetime { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsAmount { get; set; }
    }
}

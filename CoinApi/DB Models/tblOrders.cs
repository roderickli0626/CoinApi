using System.ComponentModel.DataAnnotations;

namespace CoinApi.DB_Models
{
    public class tblOrders
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int ModuleID { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Amount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}

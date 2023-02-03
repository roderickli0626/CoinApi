using System.ComponentModel.DataAnnotations;

namespace CoinApi.DB_Models
{
    public class tblOrderItems
    {
        [Key]
        public int Id { get; set; } 
        public int? ModuleId { get; set; }
        public int? Qty { get; set; }
        public decimal? Price { get; set; }
        public decimal? TotalPrice { get; set; }
        public int OrderId { get; set; }
    }
}

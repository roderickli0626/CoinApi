using System.ComponentModel.DataAnnotations;

namespace CoinApi.DB_Models
{
    public class tblCategory
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int? OrderNo { get; set; }
    }
}

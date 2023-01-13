using System.ComponentModel.DataAnnotations;

namespace CoinApi.DB_Models
{
    public class tblPayment
    {
        [Key]
        public int Id { get; set; }
        public string Method { get; set; }
        public string Description { get; set; }
        public string Settings { get; set; }
    }
}

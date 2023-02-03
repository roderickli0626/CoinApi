using System.ComponentModel.DataAnnotations;

namespace CoinApi.DB_Models
{
    public class tblPayPalConfiguration
    {
        [Key]
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Mode { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
    }
}

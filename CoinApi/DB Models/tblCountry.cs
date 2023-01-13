using System.ComponentModel.DataAnnotations;

namespace CoinApi.DB_Models
{
    public class tblCountry
    {
        [Key]
        public int CountryId { get; set; }
        public string Name { get; set; }
    }
}

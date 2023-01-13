using System.ComponentModel.DataAnnotations;

namespace CoinApi.DB_Models
{
    public partial class tblLanguageGUI
    {
        [Key]
        public int Id { get; set; }
        public string? key { get; set; }
        public string? content { get; set; } 
        public int LanguageNumber { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CoinApi.DB_Models
{
    public partial class tblSubstanceGroup
    {
        [Key]
        public int GroupNumber { get; set; }
        public int? UserID { get; set; }
        public bool? ViewYesNo { get; set; }
        public bool? StandardYesNo { get; set; }
    }
}

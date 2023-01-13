using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoinApi.DB_Models
{
    public class tblModules
    {
        [Key]
        public int ModuleID { get; set; }
        public int? GroupNumberID { get; set; }
        public string NameModule { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string ProductNumber { get; set; }
        public string File { get; set; }
        public string SubscriptionDescription { get; set; }
        public bool? IsSubscription { get; set; }
        public string Color { get; set; }
        public Nullable<System.DateTime> CreatedDatetime { get; set; }
        public Nullable<System.DateTime> UpdatedDatetime { get; set; }

        [ForeignKey("GroupNumberID")]
        public virtual tblSubstanceGroupText? tblSubstanceGroupText { get; set; }

    }
}

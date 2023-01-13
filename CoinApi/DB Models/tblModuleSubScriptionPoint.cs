using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoinApi.DB_Models
{
    public class tblModuleSubScriptionPoint
    {
        [Key]
        public int Id { get; set; }
        public string Point { get; set; }
        public int? ModuleId { get; set; }
        [ForeignKey("ModuleId")]
        public virtual tblModules? tblModules { get; set; }
    }
}

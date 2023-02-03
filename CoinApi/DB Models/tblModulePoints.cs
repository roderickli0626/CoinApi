using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoinApi.DB_Models
{
    public class tblModulePoints
    {
        [Key]
        public int Id { get; set; }
        public string Point { get; set; }
        public int? ModuleId { get; set; }
        public int? GroupNumber { get; set; }
        [ForeignKey("ModuleId")]
        public virtual tblModules? tblModules { get; set; }
    }
}

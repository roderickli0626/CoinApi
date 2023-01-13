using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoinApi.DB_Models
{
    public class tblGroupQuestions
    {
        [Key]
        public int Id { get; set; }
        public int? QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual tblQuestions? tblQuestions { get; set; }
        public int? GroupNumber { get; set; }
        [ForeignKey("GroupNumber")]
        public virtual tblSubstanceGroup? tblSubstanceGroup { get; set; }
    }
}

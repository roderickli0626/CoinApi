using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoinApi.DB_Models
{
    public class tblQuestions
    {
        [Key]
        public int Id { get; set; }
        public string Questions { get; set; }
        public int? GroupQueInfoId { get; set; }
        [ForeignKey("GroupQueInfoId")]
        public virtual tblGroupQuestionInfo? tblGroupQuestionInfo { get; set; }
        public int? GroupNumber { get; set; }
        [ForeignKey("GroupNumber")]
        public virtual tblSubstanceGroup? tblSubstanceGroup { get; set; }

        public int? languageNumber { get; set; }
        [ForeignKey("languageNumber")]
        public virtual tblLanguage? tblLanguage { get; set; }
    }
}

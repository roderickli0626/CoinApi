using System.ComponentModel.DataAnnotations;

namespace CoinApi.DB_Models
{
    public class tblGroupQuestionInfo
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
    }
}

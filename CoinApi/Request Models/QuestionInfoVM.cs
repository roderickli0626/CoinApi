using CoinApi.DB_Models;

namespace CoinApi.Request_Models
{
    public class QuestionInfoVM
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        
        public List<QuestionInfo> questionInfos { get; set; }
        public List<int> GroupNumbers { get; set; }
    }
    public class QuestionInfo
    {
        public int? Id { get; set; }
        public string Questions { get; set; }
        public int? LanguageNumber { get; set; }
        public string GroupNumber { get; set; }
    }
}

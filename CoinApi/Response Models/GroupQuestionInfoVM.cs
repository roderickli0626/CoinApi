using CoinApi.Request_Models;

namespace CoinApi.Response_Models
{
    public class GroupQuestionInfoVM
    {
        public int? Id { get; set; }
        public List<QuestionInfo> questionInfos { get; set; }
        public int? LanguageNumber { get; set; }
        public List<GroupQuestionDataVM> GroupQuestionInfo { get; set; }
    }
    public class GroupQuestionDataVM
    {
        public int? GroupNumber { get; set; }
        public int? QuestionId { get; set; }

    }
}

namespace CoinApi.Request_Models
{
    public class GroupInfoDto
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public int? LanguageId { get; set; }
        public int? UserId { get; set; }
        public bool? IsStandard { get; set; }
        public bool? IsHide { get; set; }
        public string? Language { get; set; }
    }
}

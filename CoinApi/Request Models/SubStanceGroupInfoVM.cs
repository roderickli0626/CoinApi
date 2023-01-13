namespace CoinApi.Request_Models
{
    public class SubStanceGroupInfoVM
    {
        public int Id { get; set; }
        public string SubStanceName { get; set; }
        public int? GroupNumber { get; set; }
        public int? LanguageId { get; set; }
        public bool? IsStandard { get; set; }
        public bool? IsHide { get; set; }
        public string? Language { get; set; }
        public string WavFile { get; set; }
        public string Date { get; set; }
        public string? Duration { get; set; }
        public DateTime? DateCreate { get; set; }
    }
}

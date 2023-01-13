namespace CoinApi.Response_Models
{
    public class ImportSubStanceGroupInfo
    {
        public string SubStanceName { get; set; }
        public string Group { get; set; }
        public string Language { get; set; }
        public bool? Standard { get; set; }

        public bool? Hide { get; set; }

        public string? DateCreate { get; set; }
        public string? Duration { get; set; }
    }
}

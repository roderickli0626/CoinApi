using CsvHelper.Configuration.Attributes;

namespace CoinApi.Response_Models
{
    public class ImportGroupInfo
    {
        [Index(0)]
        public string GroupName { get; set; }
        [Index(2)]
        public bool? Standard { get; set; }
        [Index(3)]
        public bool? Hide { get; set; }
        [Index(1)]
        public string? Language { get; set; }
    }
}

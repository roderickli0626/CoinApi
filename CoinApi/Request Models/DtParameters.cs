using Newtonsoft.Json;

namespace CoinApi.Request_Models
{
    public class DtParameters
    {
        [JsonProperty("draw")]
        public int Draw { get; set; }
        [JsonProperty("columns")]
        public DtColumn[] Columns { get; set; }
        [JsonProperty("order")]
        public DtOrder[] Order { get; set; }
        [JsonProperty("start")]
        public int Start { get; set; }
        [JsonProperty("length")]
        public int Length { get; set; } = 10;
        [JsonProperty("search")]
        public DtSearch Search { get; set; }
        public string SortOrder => Columns != null && Order != null && Order.Length > 0 ? (Columns[Order[0].Column].Data + (Order[0].Dir == DtOrderDir.Desc ? " " + Order[0].Dir : string.Empty)) : null;
        
    }
    public class DtColumn
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public DtSearch Search { get; set; }
    }
    public class DtOrder
    {
        public int Column { get; set; }
        public DtOrderDir Dir { get; set; }
    }
    public enum DtOrderDir
    {
        Asc,
        Desc
    }
    public class DtSearch
    {
        public string Value { get; set; }
        public bool Regex { get; set; }
    }
}

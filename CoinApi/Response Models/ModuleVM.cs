namespace CoinApi.Response_Models
{
    public class ModuleVM
    {
        public int ModuleID { get; set; }
        public int? GroupNumberID { get; set; }
        public string NameModule { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
    }
}

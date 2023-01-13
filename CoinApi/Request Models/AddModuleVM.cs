namespace CoinApi.Request_Models
{
    public class AddModuleVM
    {
        public int ModuleID { get; set; }
        public int? GroupNumberID { get; set; }
        public string NameModule { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string ProductNumber { get; set; }
        public string File { get; set; }
        public string ModulePoints { get; set; }
        public string ModuleSubscriptionPoints { get; set; }
        public string SubscriptionDescription { get; set; }
        public bool? IsSubscription { get; set; }
        public string Color { get; set; }
    }
    public class ModulePointVM
    {
        public int Id { get; set; }
        public string Point { get; set; }
    }
}

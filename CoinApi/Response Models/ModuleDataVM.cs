using CoinApi.DB_Models;
using CoinApi.Request_Models;

namespace CoinApi.Response_Models
{
    public class ModuleDataVM
    {
        public tblModules ModuleInfo { get; set; }
        public List<ModulePointVM> ModulePoints { get; set; }
        public List<ModulePointVM> ModuleSubPoints { get; set; }
        public string GroupName { get; set; }
        public string SubStanceGroupName { get; set; }
    }
    public class AllModuleDataVM
    {
        public int ModuleID { get; set; }
        public int? GroupNumberID { get; set; }
        public string NameModule { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string ProductNumber { get; set; }
        public string File { get; set; }
        public string SubscriptionDescription { get; set; }
        public bool? IsSubscription { get; set; }
        public string Color { get; set; }
        public string GroupDescription { get; set; }
        public List<ModulePointVM> ModulePoints { get; set; }
        public List<ModulePointVM> ModuleSubPoints { get; set; }
    }
}

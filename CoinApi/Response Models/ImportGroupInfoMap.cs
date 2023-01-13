using CsvHelper.Configuration;

namespace CoinApi.Response_Models
{
    public class ImportGroupInfoMap : ClassMap<ImportGroupInfo>
    {
        public ImportGroupInfoMap()
        {
            Map(p => p.GroupName).Index(0);
            Map(p => p.Language).Index(1);
            Map(p => p.Standard).Index(2);
            Map(p => p.Hide).Index(3);
        }
    }
}

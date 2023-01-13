namespace CoinApi.Helpers
{
    public class ExcelTypeHelper
    {
        public IList<ExcelPropertyType> GetPropertyTypes()
        {
            return new List<ExcelPropertyType>
            {
                new ExcelPropertyType("GroupName", "GroupName", typeof(string), false),
                new ExcelPropertyType("Language","Language", typeof(string), false),
                new ExcelPropertyType("Standard","Standard", typeof(bool), false),
                 new ExcelPropertyType("Hide","Hide" ,typeof(bool), false)
            };
        }
    }
}

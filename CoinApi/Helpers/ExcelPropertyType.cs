namespace CoinApi.Helpers
{
    public class ExcelPropertyType
    {
        public ExcelPropertyType(string headerName, string propertyName, Type propertyType, bool isRequired)
        {
            HeaderName = headerName;
            PropertyType = propertyType;
            IsRequired = isRequired;

            PropertyName = !string.IsNullOrEmpty(propertyName) ? propertyName : headerName;
        }

        public ExcelPropertyType(string headerName, Type propertyType, bool isRequired)
            : this(headerName, null, propertyType, isRequired)
        {
        }

        public string HeaderName { get; set; }

        public string PropertyName { get; set; }

        public Type PropertyType { get; set; }

        public bool IsRequired { get; set; }

        public override string ToString()
        {
            return PropertyName;
        }
    }
}

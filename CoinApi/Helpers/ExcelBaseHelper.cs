using ExcelDataReader;
using System.Globalization;

namespace CoinApi.Helpers
{
    public class ExcelBaseHelper
    {
        private readonly ExcelTypeHelper _excelTypeHelper;

        public ExcelBaseHelper(ExcelTypeHelper excelTypeHelper)
        {
            _excelTypeHelper = excelTypeHelper;
        }

        public IList<T> ParseFile<T>(Stream stream, IList<ExcelPropertyType> fields, out IList<string> errors) where T : class, new()
        {
            errors = new List<string>();

            try
            {
                using (var reader = ExcelReaderFactory.CreateCsvReader(stream))
                {
                    var count = reader.FieldCount;

                    var items = new List<T>();

                    var lastRequiredPosition = 0;

                    for (int i = fields.Count - 1; i >= 0; i--)
                    {
                        if (fields[i].IsRequired)
                        {
                            lastRequiredPosition = i + 1;
                            break;
                        }
                    }

                    if (count < lastRequiredPosition)
                    {
                        errors.Add("Invalid columns count");
                    }
                    else
                    {
                        var emptyRowsCount = 0;
                        var columns = GetColumnNames(reader);

                        for (var rowIndex = 0; rowIndex < reader.RowCount; rowIndex++)
                        {
                            try
                            {
                                var canRead = reader.Read();

                                if (!canRead)
                                {
                                    continue;
                                }

                                if (CheckIsEmptyRow(reader, fields.Count))
                                {
                                    emptyRowsCount++;

                                    if (emptyRowsCount > 25) { break; }

                                    continue;
                                }

                                emptyRowsCount = 0;

                                var item = new T();

                                var skipItem = false;

                                for (var i = 0; i < fields.Count; i++)
                                {
                                    if (columns.Length <= i)
                                    {
                                        break;
                                    }

                                    ExcelPropertyType field = fields[i];

                                    var property = typeof(T).GetProperty(field.PropertyName);

                                    var headerName = field.HeaderName;

                                    if (property == null)
                                        throw new ArgumentException($"invalid field {columns[i]}");

                                    var cellValue = reader.FieldCount < i + 1 ? null : reader.GetValue(i);

                                    if (cellValue is string cellStr)
                                    {
                                        var fieldName = field.HeaderName.ToUpperInvariant().Replace(" ", string.Empty);
                                        if (cellStr.Replace(" ", string.Empty).ToUpperInvariant().Equals(fieldName))
                                        {
                                            skipItem = true;
                                        }
                                    }

                                    if (cellValue == null)
                                    {
                                        if (field.IsRequired)
                                        {
                                            throw new ArgumentException($"Row - {rowIndex + 1} - field '{headerName}' is required");
                                        }

                                        continue;
                                    }

                                    try
                                    {
                                        var strValue = cellValue.ToString();

                                        if (field.PropertyType == typeof(DateTime))
                                        {
                                            DateTime? date = DateTime.TryParseExact((string)cellValue, "MM/dd/yyyy", null, DateTimeStyles.None, out var result) ? result : null;
                                            property.SetValue(item, date);
                                        }
                                        else
                                        {
                                            property.SetValue(item, GetPropertyValue(cellValue, field));

                                        }
                                    }
                                    catch
                                    {
                                        if (field.IsRequired)
                                        {
                                            throw;
                                        }
                                    }
                                }

                                if (!skipItem)
                                {
                                    items.Add(item);
                                }
                            }
                            catch (Exception e)
                            {
                                errors.Add(e.Message);
                            }
                        }
                    }

                    return items;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                return new List<T>();
            }
        }

        private dynamic GetPropertyValue(object cellValue, ExcelPropertyType field)
        {
            var value = Convert.ChangeType(cellValue, field.PropertyType);

            if (value is string str)
            {
                str = str.Trim();
                return str;
            }
            else
            {
                return value;
            }
        }

        private bool CheckIsEmptyRow(IExcelDataReader reader, int fieldCount)
        {
            for (var i = 0; i < fieldCount; i++)
            {
                var value = reader.FieldCount < i + 1 ? null : reader.GetValue(i);
                if (value != null) return false;
            }

            return true;
        }

        private string[] GetColumnNames(IExcelDataReader reader)
        {
            reader.Read();

            var columns = new List<string>();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                var str = reader.GetValue(i) as string;

                if (!string.IsNullOrEmpty(str))
                {
                    columns.Add(str);
                }

                else
                {
                    columns.Add(string.Empty);
                }
            }

            return columns.ToArray();
        }
    }
}

using System;
using System.Globalization;

namespace StatementIQ.Common.ElasticSearch.Tools
{
    public static class Converter
    {
        public static T ConvertTo<T>(object value, T defaultValue = default(T))
        {
            var result = defaultValue;

            if (value != null)
            {
                if (value is T resultValue)
                {
                    result = resultValue;
                }
                else
                {
                    try
                    {
                        return (T) Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        return defaultValue;
                    }
                }
            }

            return result;
        }
    }
}
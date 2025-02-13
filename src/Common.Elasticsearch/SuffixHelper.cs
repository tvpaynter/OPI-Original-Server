using System;
using System.Linq;
using System.Reflection;
using StatementIQ.Common.ElasticSearch.Tools;

namespace StatementIQ.Common.ElasticSearch
{
    public class SuffixHelper : ISuffixHelper
    {
        private static readonly string KeywordSuffix = ".keyword";

        public string FieldSuffix(string propertyName, Type dataType)
        {
            var propertyType = dataType.GetTypeInfo().DeclaredProperties.FirstOrDefault(propertyInfo =>
                propertyInfo.Name == propertyName || propertyInfo.Name.ToCamelCase() == propertyName);
            if (propertyType == null)
            {
                return string.Empty;
            }

            return propertyType.PropertyType == typeof(string) || propertyType.PropertyType.IsEnum
                ? KeywordSuffix
                : string.Empty;
        }
    }
}
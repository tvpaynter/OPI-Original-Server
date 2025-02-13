using MandateThat;

namespace StatementIQ.Common.ElasticSearch.Tools
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string propertyName)
        {
            Mandate.That(propertyName).IsEmpty();
            return propertyName.Length > 1
                ? $"{propertyName.Substring(0, 1).ToLowerInvariant()}{propertyName.Substring(1)}"
                : propertyName.ToLowerInvariant();
        }
    }
}
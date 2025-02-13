using System.Collections.Generic;
using System.Linq;
using MandateThat;
using static System.String;

namespace StatementIQ.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddCommaSeparatedValues(this ICollection<string> collection, string commaSeparatedValues)
        {
            Mandate.That(collection, nameof(collection)).IsNotNull();
            Mandate.That(commaSeparatedValues, nameof(commaSeparatedValues)).IsNotNullOrWhiteSpace();

            var values = commaSeparatedValues.Split(',');

            foreach (var value in values.Where(value => !IsNullOrEmpty(value))) collection.Add(value);
        }
    }
}
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using MandateThat;

namespace StatementIQ.Extensions
{
    public static class NameValueCollectionExtensions
    {
        public static IDictionary<string, string> ToDictionary(this NameValueCollection col)
        {
            Mandate.That(col, nameof(col)).IsNotNull();

            return col.AllKeys.ToDictionary(k => k, k => col[k]);
        }
    }
}
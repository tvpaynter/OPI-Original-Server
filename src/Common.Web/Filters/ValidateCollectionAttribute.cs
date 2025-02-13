using System;
using System.Collections.Generic;
using System.Linq;
using MandateThat;

namespace StatementIQ.Common.Web.Filters
{
    public class ValidateCollectionAttribute : Attribute
    {
        public ValidateCollectionAttribute(params string[] collectionNames)
        {
            Mandate.That(collectionNames, nameof(collectionNames)).IsNotNull();

            if (!collectionNames.Any() || collectionNames.Any(string.IsNullOrEmpty))
            {
                throw new ArgumentException(nameof(collectionNames));
            }

            CollectionNames = collectionNames;
        }

        public IEnumerable<string> CollectionNames { get; }
    }
}
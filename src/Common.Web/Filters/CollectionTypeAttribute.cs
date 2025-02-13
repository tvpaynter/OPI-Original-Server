using System;
using MandateThat;

namespace StatementIQ.Common.Web.Filters
{
    public class CollectionTypeAttribute : Attribute
    {
        public CollectionTypeAttribute(string collectionName)
        {
            Mandate.That(collectionName, nameof(collectionName)).IsNotNullOrEmpty();

            CollectionName = collectionName;
        }

        public string CollectionName { get; }
    }
}
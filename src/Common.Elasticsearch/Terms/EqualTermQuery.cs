using System;
using MandateThat;
using Nest;
using StatementIQ.Common.ElasticSearch.Tools;

namespace StatementIQ.Common.ElasticSearch.Terms
{
    public sealed class EqualTermQuery : ITermQueryFactory
    {
        private readonly ISuffixHelper _suffixHelper;

        public EqualTermQuery(ISuffixHelper suffixHelper)
        {
            _suffixHelper = suffixHelper;
        }

        public string Separator { get; } = "=";

        public QueryBase GetQuery(string field, object value, Type dataType)
        {
            Mandate.That(field, nameof(field)).IsNotNullOrWhiteSpace();
            Mandate.That(field, nameof(field)).IsNotNullOrWhiteSpace();

            return new TermQuery
            {
                Field = $"{field.ToCamelCase()}{_suffixHelper.FieldSuffix(field, dataType)}",
                Value = value
            };
        }
    }
}
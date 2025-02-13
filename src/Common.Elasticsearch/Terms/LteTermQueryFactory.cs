using System;
using System.Linq;
using System.Reflection;
using MandateThat;
using Nest;
using StatementIQ.Common.Data.Extensions;
using StatementIQ.Common.ElasticSearch.Exceptions;
using StatementIQ.Common.ElasticSearch.Tools;

namespace StatementIQ.Common.ElasticSearch.Terms
{
    public sealed class LteTermQueryFactory : ITermQueryFactory
    {
        public string Separator { get; } = "<=";
        
        private bool IsNullableValueType<T>(T obj)
        {
            return default(T) == null && typeof(T).BaseType != null && "ValueType".Equals(typeof(T).BaseType.Name);
        }
        
        public QueryBase GetQuery(string field, object value, Type dataType)
        {
            Mandate.That(field, nameof(field)).IsNotNullOrWhiteSpace();
            Mandate.That(field, nameof(field)).IsNotNullOrWhiteSpace();

            var propertyType = dataType.GetTypeInfo().DeclaredProperties.FirstOrDefault(propertyInfo =>
                propertyInfo.Name == field || propertyInfo.Name.ToCamelCase() == field);

            if (propertyType == null)
            {
                throw new ElasticSearchTermException($"Property type of field: '{field}' is null");
            }

            var typeCode = Type.GetTypeCode(propertyType.PropertyType.IsNullable()
                ? propertyType.PropertyType.GenericTypeArguments.FirstOrDefault()
                : propertyType.PropertyType);
            
            switch (typeCode)
            {
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    return new LongRangeQuery
                    {
                        Field = field,
                        LessThanOrEqualTo = Convert.ToInt64(value)
                    };
                case TypeCode.Decimal:
                case TypeCode.Double:
                    return new NumericRangeQuery
                    {
                        Field = field,
                        LessThanOrEqualTo = Convert.ToDouble(value)
                    };
                case TypeCode.DateTime:
                    return new DateRangeQuery
                    {
                        Field = field,
                        LessThanOrEqualTo = Convert.ToString(value)
                    };
                case TypeCode.String:
                    return new TermRangeQuery
                    {
                        Field = field,
                        LessThanOrEqualTo = Convert.ToString(value)
                    };
                default:
                    throw new ElasticSearchTermException($"Type :{propertyType.PropertyType.Name} of property {field} does not support");
            }
        }
    }
}
using System.Collections.Generic;

namespace StatementIQ.Common.Data
{
    public static class UpdateHelper
    {
        public static void ApplyFieldValueTo<T>(this IDictionary<string, object> input, NameValueObject<T>
            result, string fieldName) where T : new()
        {
            if (input.TryGetValue(fieldName, out var value))
            {
                result[fieldName] = value;
            }
        }

        public static void ApplyFieldValuesTo<TIn, TOut>(this NameValueObject<TIn> input,
            NameValueObject<TOut> result)
            where TIn : new()
            where TOut : new()
        {
            foreach (var propertyName in input.GetPatchablePropertiesNames())
            {
                input.ApplyFieldValueTo(result, propertyName);
            }
        }
    }
}
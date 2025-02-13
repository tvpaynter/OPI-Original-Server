using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace StatementIQ.Extensions
{
    public static class TypeExtensions
    {
        public static IEnumerable<string> GetPropertiesNames<T>(this Type input) where T : Attribute
        {
            return input.GetProperties()
                .Where(prop => prop.IsDefined(typeof(T), false))
                .Select(x => x.Name);
        }

        public static PropertyInfo GetPropertyInfo(this Type input, string fieldName)
        {
            return input.GetProperties().SingleOrDefault(x =>
                (x.GetCustomAttribute(typeof(JsonPropertyNameAttribute)) as JsonPropertyNameAttribute)?.Name ==
                fieldName || x.Name == fieldName);
        }
    }
}
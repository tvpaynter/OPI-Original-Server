using System;
using System.Collections.Generic;
using System.Linq;

namespace StatementIQ.Common.ElasticSearch.Tools
{
    public static class EnumUtilities
    {
        private static List<T> GetList<T>() where T : struct
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>().ToList();
        }
        public static T? GetEnum<T>(string label) where T : struct
        {
            var list = GetList<T>();

            bool Filter(T c) => c.ToString().Trim().ToLowerInvariant() == label?.Trim().ToLowerInvariant();

            if (!list.Any(Filter))
            {
                return null;
            }

            return list.FirstOrDefault(Filter);
        }
    }
}
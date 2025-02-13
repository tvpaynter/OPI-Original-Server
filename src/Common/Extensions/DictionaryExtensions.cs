using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MandateThat;

namespace StatementIQ.Extensions
{
    public static class DictionaryExtensions
    {
        public static IReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary)
        {
            Mandate.That(dictionary, nameof(dictionary)).IsNotNull();
            return new ReadOnlyDictionary<TKey, TValue>(dictionary);
        }

        public static T MergeLeft<T, TK, TV>(this T me, params IDictionary<TK, TV>[] others)
            where T : IDictionary<TK, TV>, new()
        {
            Mandate.That(others, nameof(others)).IsNotNull();

            var target = new List<IDictionary<TK, TV>> {me}.Concat(others);
            var newMap = new T();

            target.ToList()
                .ForEach(src => { src.ToList().ForEach(p => { newMap[p.Key] = p.Value; }); });

            return newMap;
        }
    }
}
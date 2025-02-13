using System.Collections.Generic;

namespace StatementIQ.Common.Data
{
    public interface IUpdateValues
    {
        IEnumerable<string> GetFieldNames();

        IReadOnlyDictionary<string, object> GetFields();

        bool ContainsKey(string key);
        
        void Add(string key, object value);
    }
}
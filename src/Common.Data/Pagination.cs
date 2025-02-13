using System.Collections.Generic;

namespace StatementIQ.Common.Data
{
    public class Pagination<TResults>
    {
        public long Count { get; set; }
        public IEnumerable<TResults> Results { get; set; }
    }
}
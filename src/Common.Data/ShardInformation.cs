using System;
using System.Data.Common;

namespace StatementIQ.Common.Data
{
    public class ShardInformation
    {
        public int ShardId { get; set; }
        public string ConnectionString { get; set; }
        public Func<DbConnection> Connection { get; set; }
    }
}
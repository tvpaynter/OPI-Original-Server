using System;

namespace StatementIQ.Common.Web.Models
{
    public class Statistic
    {
        public long? UserId { get; set; }
        public long? HierarchyId { get; set; }

        public long? SessionId { get; set; }
        public string Token { get; set; }
        public string UserAgent { get; set; }
        public string Url { get; set; }
        public string Ip { get; set; }
        public string Body { get; set; }
        public long ExecutionTime { get; set; }
        public DateTime ExecutedTime { get; set; }

        public string ServiceName { get; set; }

        public string Method { get; set; }
    }
}
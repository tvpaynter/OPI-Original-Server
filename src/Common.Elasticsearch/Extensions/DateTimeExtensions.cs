using System;

namespace StatementIQ.Common.ElasticSearch.Extensions
{
    public static class DateTimeExtensions
    {
        public static long GetTimeStamp(this DateTime dateTime)
        {
            return new DateTimeOffset(dateTime).ToUnixTimeSeconds();
        }
    }
}
using System;
using System.Collections.Generic;
using StatementIQ.Properties;

namespace StatementIQ.Extensions
{
    public static class LongExtensions
    {
        private static readonly string
            Base62CharList = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string ToBase62(this ulong input)
        {
            var clistarr = Base62CharList.ToCharArray();
            var result = new Stack<char>();

            while (input != 0)
            {
                result.Push(clistarr[input % 62]);
                input /= 62;
            }

            return new string(result.ToArray());
        }
    }
}
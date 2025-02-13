using System.Linq;

namespace StatementIQ.Common.Data.Extensions
{
    public static class StringExtension
    {
        public static string FirstCharToUpper(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return input.First().ToString().ToUpper() + input.Substring(1);
            }

            return input;
        }
    }
}
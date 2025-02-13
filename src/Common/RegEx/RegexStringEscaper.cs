using System.Text;
using MandateThat;

namespace StatementIQ.RegEx
{
    /// <summary>   A RegEx string escaper. </summary>
    public static class RegexStringEscaper
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Escapes. </summary>
        /// <param name="value">            The value. </param>
        /// <param name="escapeBackslash">  True to escape backslash. </param>
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string Escape(string value, bool escapeBackslash)
        {
            Mandate.That(value, nameof(value)).IsNotNullOrWhiteSpace();

            var resultBuilder = new StringBuilder(value);
            if (escapeBackslash) resultBuilder.Replace("\\", "\\\\");

            string[] oldValues =
            {
                "^", "$", ".", "|", "?", "*", "+", "(", ")", "[", "]", "{", "}"
            };
            string[] newValues =
            {
                "\\^", "\\$", "\\.", "\\|", "\\?", "\\*", "\\+", "\\(", "\\)", "\\[", "\\]", "\\{", "\\}"
            };

            resultBuilder.ReplaceMany(oldValues, newValues);
            return resultBuilder.ToString();
        }
    }
}
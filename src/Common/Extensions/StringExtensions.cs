using System;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using MandateThat;

namespace StatementIQ.Extensions
{
    public static class StringExtensions
    {
        private const string Base62CharList = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string CompactWhiteSpaces(this string input)
        {
            Mandate.That(input, nameof(input)).IsNotNullOrWhiteSpace();

            var builder = new StringBuilder(input);
            builder.CompactWhiteSpaces();

            return builder.ToString().Trim();
        }

        public static long FromBase62(this string input)
        {
            Mandate.That(input, nameof(input)).IsNotNullOrWhiteSpace();

            var reversed = input.Reverse();
            var result = 0L;
            var pos = 0;

            foreach (var c in reversed)
            {
                result += Base62CharList.IndexOf(c) * (long) Math.Pow(62, pos);
                pos++;
            }

            return result;
        }

        public static string RemoveSpecialCharacters(this string input)
        {
            Mandate.That(input, nameof(input)).IsNotNullOrWhiteSpace();

            var buffer = new char[input.Length];
            var idx = 0;

            foreach (var c in input)
                if (c >= '0' && c <= '9'
                    || c >= 'A' && c <= 'Z'
                    || c >= 'a' && c <= 'z')
                {
                    buffer[idx] = c;
                    idx++;
                }

            return new string(buffer, 0, idx);
        }

        public static string GetAllDigital(this string input)
        {
            Mandate.That(input, nameof(input)).IsNotNullOrWhiteSpace();

            return new string(input.Where(char.IsDigit).ToArray());
        }

        public static SecureString ToSecureString(this string input)
        {
            Mandate.That(input, nameof(input)).IsNotNullOrWhiteSpace();

            return input.Aggregate(new SecureString(), AppendChar, MakeReadOnly);
        }

        public static string ToCamelCase(this string input)
        {
            if (input == null || input.Length < 2) return input;

            var words = input.Split(
                new char[] { }, StringSplitOptions.RemoveEmptyEntries);

            var sb = new StringBuilder();
            sb.Append(words.First().Substring(0, 1).ToLower(CultureInfo.InvariantCulture));
            sb.Append(words[0].Substring(1));

            for (var i = 1; i < words.Length; i++)
            {
                sb.Append(words[i].Substring(0, 1).ToUpper(CultureInfo.InvariantCulture));
                sb.Append(words[i].Substring(1));
            }

            return sb.ToString();
        }

        public static string ToPascalCase(this string input)
        {
            if (input == null) return input;

            if (input.Length < 2) return input.ToUpper(CultureInfo.InvariantCulture);

            var words = input.Split(
                new char[] { }, StringSplitOptions.RemoveEmptyEntries);

            var sb = new StringBuilder();

            foreach (var word in words)
            {
                sb.Append(word.Substring(0, 1).ToUpper(CultureInfo.InvariantCulture));
                sb.Append(word.Substring(1));
            }

            return sb.ToString();
        }

        private static SecureString MakeReadOnly(SecureString secureString)
        {
            Mandate.That(secureString, nameof(secureString)).IsNotNull();

            secureString.MakeReadOnly();

            return secureString;
        }

        private static SecureString AppendChar(SecureString secureString, char c)
        {
            Mandate.That(secureString, nameof(secureString)).IsNotNull();

            secureString.AppendChar(c);

            return secureString;
        }
    }
}
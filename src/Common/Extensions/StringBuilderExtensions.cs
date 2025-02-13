using System.Text;
using MandateThat;
using static System.Char;

namespace StatementIQ.Extensions
{
    public static class StringBuilderExtensions
    {
        public static void CompactWhiteSpaces(this StringBuilder sb)
        {
            Mandate.That(sb, nameof(sb)).IsNotNull();

            if (sb.Length == 0) return;

            var start = 0;

            while (start < sb.Length)
            {
                if (IsWhiteSpace(sb[start]))
                {
                    start++;
                    continue;
                }

                break;
            }

            if (start == sb.Length)
            {
                sb.Length = 0;
                return;
            }

            var end = sb.Length - 1;

            while (end >= 0)
            {
                if (IsWhiteSpace(sb[end]))
                {
                    end--;
                    continue;
                }

                break;
            }

            var dest = 0;
            var previousIsWhitespace = false;

            for (var i = start; i <= end; i++)
            {
                if (IsWhiteSpace(sb[i]))
                {
                    if (previousIsWhitespace) continue;

                    previousIsWhitespace = true;
                    sb[dest] = ' ';
                    dest++;

                    continue;
                }

                previousIsWhitespace = false;
                sb[dest] = sb[i];
                dest++;
            }

            sb.Length = dest;
        }
    }
}
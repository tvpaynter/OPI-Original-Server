using System;
using System.Text.RegularExpressions;
using MandateThat;

namespace StatementIQ.RegEx.RegexEngine
{
    /// <summary>   A RegEx cache. This class cannot be inherited. </summary>
    public sealed class RegexCache
    {
        /// <summary>   The lock object. </summary>
        private readonly object _lockObject = new object();

        /// <summary>   True if has value, false if not. </summary>
        private bool hasValue;

        /// <summary>   The key. </summary>
        private Key key;

        /// <summary>   The RegEx. </summary>
        private Regex regex;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets the already cached value for a key, or calculates the value and stores it.
        /// </summary>
        /// <exception cref="ArgumentNullException">    . </exception>
        /// <param name="pattern">  The pattern used to create the regular expression. </param>
        /// <param name="options">  The options for regex. </param>
        /// <returns>   The calculated or cached value. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public Regex Get(string pattern, RegexOptions options)
        {
            Mandate.That(pattern, nameof(pattern)).IsNotNull();

            lock (_lockObject)
            {
                var current = new Key(pattern, options);
                if (hasValue && current.Equals(key)) return regex;

                regex = new Regex(pattern, options);
                key = current;
                hasValue = true;
                return regex;
            }
        }

        /// <summary>   A key. </summary>
        private class Key
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>
            ///     Initializes a new instance of the StatementIQ.Common.RegexEngine.RegexCache.Key class.
            /// </summary>
            /// <param name="pattern">  The pattern. </param>
            /// <param name="options">  The options. </param>
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            public Key(string pattern, RegexOptions options)
            {
                Mandate.That(!string.IsNullOrWhiteSpace(pattern), "pattern cannot be null");

                Pattern = pattern;
                Options = options;
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets the pattern. </summary>
            /// <value> The pattern. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public string Pattern { get; }

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Gets options for controlling the operation. </summary>
            /// <value> The options. </value>
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            public RegexOptions Options { get; }

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Determines whether the specified object is equal to the current object. </summary>
            /// <param name="obj">  The object to compare with the current object. </param>
            /// <returns>
            ///     true if the specified object  is equal to the current object; otherwise, false.
            /// </returns>
            /// <seealso cref="M:System.Object.Equals(object)" />
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            public override bool Equals(object obj)
            {
                Mandate.That(obj != null, "obj cannot be null");
                return obj is Key key && string.Equals(key.Pattern, Pattern, StringComparison.Ordinal) &&
                       key.Options == Options;
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            /// <summary>   Serves as the default hash function. </summary>
            /// <returns>   A hash code for the current object. </returns>
            /// <seealso cref="M:System.Object.GetHashCode()" />
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            public override int GetHashCode()
            {
                return StringComparer.Ordinal.GetHashCode(Pattern) ^ Options.GetHashCode();
            }
        }
    }
}
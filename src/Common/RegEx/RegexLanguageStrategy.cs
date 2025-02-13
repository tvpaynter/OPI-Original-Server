using System;

namespace StatementIQ.RegEx
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Values that represent the RegEx languages. </summary>
    /// <remarks>   StatementIQ, 5/14/2020. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public enum RegexLanguage
    {
        /// <summary>   An enum constant representing the dot net option. </summary>
        DotNet,

        /// <summary>   An enum constant representing the ecma script option. </summary>
        ECMAScript
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A RegEx language strategy. This class cannot be inherited. </summary>
    /// <remarks>   StatementIQ, 5/14/2020. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public sealed class RegexLanguageStrategy
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the StatementIQ.Common.Library.RegEx.RegexLanguageStrategy
        ///     class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="regexLanguage">    The RegEx language. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexLanguageStrategy(RegexLanguage regexLanguage)
        {
            RegexLanguage = regexLanguage;

            Stringifier = RegexLanguage switch
            {
                RegexLanguage.DotNet => new DotNetRegexStringifier(),
                RegexLanguage.ECMAScript => new ECMAScriptRegexStringifier(),
                _ => throw new NotSupportedException()
            };
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the RegEx language. </summary>
        /// <value> The RegEx language. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public RegexLanguage RegexLanguage { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the stringified representation. </summary>
        /// <value> The stringified representation. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public IRegexStringifier Stringifier { get; }
    }
}
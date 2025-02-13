
namespace StatementIQ.RegEx.Exceptions
{
    using System;
    using System.Runtime.Serialization;
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Exception for signalling RegEx language not supported errors. </summary>
    /// <remarks>   StatementIQ, 5/14/2020. </remarks>
    /// <seealso cref="T:System.NotSupportedException" />
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RegexLanguageNotSupportedException : NotSupportedException
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the
        ///     StatementIQ.Common.Library.RegEx.RegexLanguageNotSupportedException class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexLanguageNotSupportedException()
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the
        ///     StatementIQ.Common.Library.RegEx.RegexLanguageNotSupportedException class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="regexLanguage">    The RegEx language. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexLanguageNotSupportedException(RegexLanguage regexLanguage)
        {
            RegexLanguage = regexLanguage;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the
        ///     StatementIQ.Common.Library.RegEx.RegexLanguageNotSupportedException class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="regexLanguage">    The RegEx language. </param>
        /// <param name="message">          The message. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexLanguageNotSupportedException(RegexLanguage regexLanguage, string message)
            : base(message)
        {
            RegexLanguage = regexLanguage;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the
        ///     StatementIQ.Common.Library.RegEx.RegexLanguageNotSupportedException class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="regexLanguage">    The RegEx language. </param>
        /// <param name="message">          The message. </param>
        /// <param name="innerException">   The inner exception. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexLanguageNotSupportedException(RegexLanguage regexLanguage, string message, Exception innerException)
            : base(message, innerException)
        {
            RegexLanguage = regexLanguage;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the
        ///     StatementIQ.Common.Library.RegEx.RegexLanguageNotSupportedException class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="regexLanguage">    The RegEx language. </param>
        /// <param name="innerException">   The inner exception. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected RegexLanguageNotSupportedException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the RegEx language. </summary>
        /// <value> The RegEx language. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public RegexLanguage RegexLanguage { get; }
    }
}
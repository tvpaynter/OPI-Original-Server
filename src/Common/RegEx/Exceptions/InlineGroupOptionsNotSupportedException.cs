
namespace StatementIQ.RegEx.Exceptions
{
    using System;
    using System.Runtime.Serialization;
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Exception for signaling inline group options not supported errors. </summary>
    /// <remarks>   StatementIQ, 5/14/2020. </remarks>
    /// <seealso cref="T:StatementIQ.RegEx.Exceptions.RegexLanguageNotSupportedException" />
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class InlineGroupOptionsNotSupportedException : RegexLanguageNotSupportedException
    {
        /// <summary>   The message. </summary>
        protected const string _message = "Inline group options is not supported by {0}.";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the
        ///     StatementIQ.Common.Library.RegEx.Exceptions.InlineGroupOptionsNotSupportedException class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="regexLanguage">    The RegEx language. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public InlineGroupOptionsNotSupportedException(RegexLanguage regexLanguage)
            : base(regexLanguage, string.Format(_message, regexLanguage))
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the
        ///     StatementIQ.Common.Library.RegEx.Exceptions.InlineGroupOptionsNotSupportedException class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="regexLanguage">    The RegEx language. </param>
        /// <param name="innerException">   The inner exception. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public InlineGroupOptionsNotSupportedException(RegexLanguage regexLanguage, Exception innerException)
            : base(regexLanguage, string.Format(_message, regexLanguage), innerException)
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the
        ///     StatementIQ.Common.Library.RegEx.InlineGroupOptionsNotSupportedException class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="regexLanguage">    The RegEx language. </param>
        /// <param name="innerException">   The inner exception. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected InlineGroupOptionsNotSupportedException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
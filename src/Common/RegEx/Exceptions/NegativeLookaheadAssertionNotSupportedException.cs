
namespace StatementIQ.RegEx.Exceptions
{
    using System;
    using System.Runtime.Serialization;
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     Exception for signaling negative look-ahead assertion not supported errors.
    /// </summary>
    /// <remarks>   StatementIQ, 5/14/2020. </remarks>
    /// <seealso cref="T:StatementIQ.RegEx.Exceptions.RegexLanguageNotSupportedException" />
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class NegativeLookaheadAssertionNotSupportedException : RegexLanguageNotSupportedException
    {
        /// <summary>   The message. </summary>
        protected const string _message = "Negative lookahead assertion is not supported by {0}.";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the
        ///     StatementIQ.Common.Library.RegEx.Exceptions.NegativeLookaheadAssertionNotSupportedException
        ///     class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="regexLanguage">    The RegEx language. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public NegativeLookaheadAssertionNotSupportedException(RegexLanguage regexLanguage)
            : base(regexLanguage, string.Format(_message, regexLanguage))
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the
        ///     StatementIQ.Common.Library.RegEx.Exceptions.NegativeLookaheadAssertionNotSupportedException
        ///     class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="regexLanguage">    The RegEx language. </param>
        /// <param name="innerException">   The inner exception. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public NegativeLookaheadAssertionNotSupportedException(RegexLanguage regexLanguage, Exception innerException)
            : base(regexLanguage, string.Format(_message, regexLanguage), innerException)
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the
        ///     StatementIQ.Common.Library.RegEx.NegativeLookaheadAssertionNotSupportedException class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="regexLanguage">    The RegEx language. </param>
        /// <param name="innerException">   The inner exception. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected NegativeLookaheadAssertionNotSupportedException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
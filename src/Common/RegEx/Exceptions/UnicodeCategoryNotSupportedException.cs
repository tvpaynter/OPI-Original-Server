
namespace StatementIQ.RegEx.Exceptions
{
    using System;
    using System.Runtime.Serialization;
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Exception for signalling unicode category not supported errors. </summary>
    /// <remarks>   StatementIQ, 5/14/2020. </remarks>
    /// <seealso cref="T:StatementIQ.RegEx.Exceptions.RegexLanguageNotSupportedException" />
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class UnicodeCategoryNotSupportedException : RegexLanguageNotSupportedException
    {
        /// <summary>   The message. </summary>
        protected const string _message = "Unicode category is not supported by {0}.";

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the
        ///     StatementIQ.Common.Library.RegEx.Exceptions.UnicodeCategoryNotSupportedException class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="regexLanguage">    The RegEx language. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public UnicodeCategoryNotSupportedException(RegexLanguage regexLanguage)
            : base(regexLanguage, string.Format(_message, regexLanguage))
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the
        ///     StatementIQ.Common.Library.RegEx.Exceptions.UnicodeCategoryNotSupportedException class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="regexLanguage">    The RegEx language. </param>
        /// <param name="innerException">   The inner exception. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public UnicodeCategoryNotSupportedException(RegexLanguage regexLanguage, Exception innerException)
            : base(regexLanguage, string.Format(_message, regexLanguage), innerException)
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the
        ///     StatementIQ.Common.Library.RegEx.UnicodeCategoryNotSupportedException class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="regexLanguage">    The RegEx language. </param>
        /// <param name="innerException">   The inner exception. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected UnicodeCategoryNotSupportedException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}

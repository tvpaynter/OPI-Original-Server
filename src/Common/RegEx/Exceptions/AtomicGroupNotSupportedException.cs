
namespace StatementIQ.RegEx.Exceptions
{
    using System;
    using System.Runtime.Serialization;
        
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Exception for signalling atomic group not supported errors. </summary>
    /// <remarks>   StatementIQ, 5/14/2020. </remarks>
    /// <seealso cref="T:StatementIQ.Common.Library.RegEx.RegexLanguageNotSupportedException" />
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [Serializable]
    public class AtomicGroupNotSupportedException : RegexLanguageNotSupportedException
    {
        /// <summary>   The message. </summary>
        protected const string _message = "Atomic group is not supported by {0}.";


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the
        ///     StatementIQ.Common.Library.RegEx.AtomicGroupNotSupportedException class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="regexLanguage">    The RegEx language. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public AtomicGroupNotSupportedException(RegexLanguage regexLanguage)
            : base(regexLanguage, string.Format(_message, regexLanguage))
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the
        ///     StatementIQ.Common.Library.RegEx.AtomicGroupNotSupportedException class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="regexLanguage">    The RegEx language. </param>
        /// <param name="innerException">   The inner exception. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public AtomicGroupNotSupportedException(RegexLanguage regexLanguage, Exception innerException)
            : base(regexLanguage, string.Format(_message, regexLanguage), innerException)
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the
        ///     StatementIQ.Common.Library.RegEx.AtomicGroupNotSupportedException class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="regexLanguage">    The RegEx language. </param>
        /// <param name="innerException">   The inner exception. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected AtomicGroupNotSupportedException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
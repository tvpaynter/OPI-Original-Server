using System;
using System.Runtime.Serialization;

#nullable enable
namespace StatementIQ.Common.Test.XUnit
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   The exception to throw to register a skipped test. </summary>
    /// <remarks>   StatementIQ, 5/15/2020. </remarks>
    /// <seealso cref="T:System.Exception" />
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [Serializable]
    public class SkipException : Exception
    {
        /// <inheritdoc cref="SkipException(string?, Exception)" />
        public SkipException()
        {
        }

        /// <inheritdoc cref="SkipException(string?, Exception)" />
        public SkipException(string? reason)
            : base(reason)
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Initializes a new instance of the <see cref="SkipException" /> class. </summary>
        /// <remarks>   StatementIQ, 5/15/2020. </remarks>
        /// <param name="reason">           The reason the test is skipped. </param>
        /// <param name="innerException">   The inner exception. </param>
        /// <seealso cref="M:StatementIQ.Common.Test.XUnit.SkipException.SkipException(string?,Exception)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public SkipException(string? reason, Exception innerException)
            : base(reason, innerException)
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Initializes a new instance of the <see cref="SkipException" /> class. </summary>
        /// <inheritdoc
        ///     cref="Exception(System.Runtime.Serialization.SerializationInfo, System.Runtime.Serialization.StreamingContext)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected SkipException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
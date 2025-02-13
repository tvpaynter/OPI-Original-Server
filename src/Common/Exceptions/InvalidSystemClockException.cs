using System;
using System.Runtime.Serialization;

namespace StatementIQ.Exceptions
{
    [Serializable]
    public class InvalidSystemClockException : Exception
    {
        protected InvalidSystemClockException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }

        public InvalidSystemClockException(string message)
            : base(message)
        {
        }
    }
}
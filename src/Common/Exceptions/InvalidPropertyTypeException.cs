using System;
using System.Runtime.Serialization;

namespace StatementIQ.Exceptions
{
    [Serializable]
    public class InvalidPropertyTypeException : Exception
    {
        public InvalidPropertyTypeException(string propertyName, string message) : base(message)
        {
            PropertyName = propertyName;
        }

        protected InvalidPropertyTypeException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
            base(serializationInfo, streamingContext)
        {
        }

        public string PropertyName { get; set; }
    }
}
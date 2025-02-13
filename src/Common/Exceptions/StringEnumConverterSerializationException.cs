using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace StatementIQ.Exceptions
{
    [Serializable]
    public class StringEnumConverterSerializationException : JsonException
    {
        protected StringEnumConverterSerializationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public StringEnumConverterSerializationException(string message)
            : base(message)
        {
        }
    }
}
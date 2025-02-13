using StatementIQ.Exceptions;
using Xunit;

namespace StatementIQ.Common.Test.Exceptions
{
    public class StringEnumConverterSerializationExceptionTests
    {
        [Fact]
        public void Should_Create_StringEnumConverterSerializationException_With_Message()
        {
            var message = "Error";
            var exception = new StringEnumConverterSerializationException(message);

            Assert.Equal(message, exception.Message);
        }
    }
}
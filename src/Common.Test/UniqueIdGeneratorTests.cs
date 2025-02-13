using System;
using System.Threading.Tasks;
using Xunit;

namespace StatementIQ.Common.Test
{
    public class UniqueIdGeneratorTests
    {
        [Fact]
        public void Should_Return_NextId_With_Valid_Value()
        {
            var uniqueIdGenerator = new UniqueIdGenerator(1);
            var id = uniqueIdGenerator.NextId();

            Assert.True(id != 1);
        }

        [Fact]
        public async Task Should_Return_NextIdAsync_With_Valid_Values()
        {
            var uniqueIdGenerator = new UniqueIdGenerator(1);
            var id = await uniqueIdGenerator.NextIdAsync();

            Assert.True(id != 1);
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_WorkerId_Is_Less_Than_Zero()
        {
            Assert.Throws<ArgumentException>(() => { new UniqueIdGenerator(-1); });
        }
    }
}
using System;
using LyonL.Extensions;
using StatementIQ.Common.Test.Enums;
using Xunit;

namespace StatementIQ.Common.Test.Extensions
{
    public class EnumExtensionsTests
    {
        [Fact]
        public void ToEnumType_throws_if_enum_not_contain_value()
        {
            //Act
            void Actual () => 8.ToEnumType<WeekDays>();
            
            //Assert
            Assert.Throws<InvalidCastException>(Actual);
        }
        
        [Fact]
        public void ConvertTo()
        {
            //Arrange
            var enum1 = TestEnum1.ValueA;

            //Act
            var enum2 = enum1.ConvertTo<TestEnum2>();

            //Assert
            Assert.Equal((int) enum1, (int) enum2);
        }
        
        [Fact]
        public void ConvertTo_throws_when_value_is_not_present_in_result_enum()
        {
            //Arrange
            var enum1 = TestEnum1.ValueC;

            //Act
            void Actual () => enum1.ConvertTo<TestEnum2>();

            //Assert
            Assert.Throws<ArgumentException>(Actual);
        }
        
        [Fact]
        public void ConvertTo_flags_enum()
        {
            //Arrange
            var enum1 = TestEnum1.ValueA;

            //Act
            var enum2 = enum1.ConvertTo<FlagsEnum>();

            //Assert
            Assert.Equal((int) enum1, (int) enum2);
        }
    }
}
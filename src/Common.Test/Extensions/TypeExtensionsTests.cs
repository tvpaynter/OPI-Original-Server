using System;
using System.Linq;
using System.Text.Json.Serialization;
using StatementIQ.Extensions;
using Xunit;

namespace StatementIQ.Common.Test.Extensions
{
    public class TypeExtensionsTests
    {
        private class TestAttribute : Attribute
        {
        }

        private class TestClass
        {
            [TestAttribute] public string PropertyWithAttribute { get; set; }

            [JsonPropertyName("test")] public string PropertyWithJsonPropertyNameTest { get; set; }

            public string PropertyWithoutAttribute { get; set; }
        }

        [Fact]
        public void GetPropertiesNames_should_return_collection_with_single_item()
        {
            //Act
            var propertiesNames = typeof(TestClass).GetPropertiesNames<TestAttribute>();

            //Assert
            Assert.Single(propertiesNames);
        }

        [Fact]
        public void GetPropertyInfo_should_return_related_property_by_attribute_name()
        {
            //Act
            var propertyInfo = typeof(TestClass).GetPropertyInfo(nameof(TestClass.PropertyWithoutAttribute));

            //Assert
            Assert.Equal(nameof(TestClass.PropertyWithoutAttribute), propertyInfo.Name);
        }

        [Fact]
        public void GetPropertyInfo_should_return_related_property()
        {
            //Act
            var propertyInfo = typeof(TestClass).GetPropertyInfo("test");

            //Assert
            Assert.Equal(nameof(TestClass.PropertyWithJsonPropertyNameTest), propertyInfo.Name);
        }
        
        [Fact]
        public void GetPropertyInfo_should_return_null_when_invalid_propertyPassedIn()
        {
            //Act
            var propertyInfo = typeof(TestClass).GetPropertyInfo("test1");

            //Assert
            Assert.Null(propertyInfo);
        }

        [Fact]
        public void GetPropertiesNames()
        {
            //Arrange
            const string propertyName = "PropertyWithAttribute";

            //Act
            var propertiesNames = typeof(TestClass).GetPropertiesNames<TestAttribute>();

            //Assert
            Assert.Equal(propertyName, propertiesNames.First());
        }
    }
}
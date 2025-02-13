using System;
using System.Collections.Generic;
using StatementIQ.Extensions;
using Xunit;

namespace StatementIQ.Common.Test.Extensions
{
    public class CollectionExtensionsTests
    {
        [Fact]
        public void Should_AddCommaSeparatedValues_To_An_Empty_String_Collection()
        {
            var commaSeparatedValues = "statementiq,llc,rocks,the,market";

            var target = new List<string>();
            target.AddCommaSeparatedValues(commaSeparatedValues);

            Assert.Equal(target.Count, commaSeparatedValues.Split(',').Length);
        }

        [Fact]
        public void Should_AddCommaSeparatedValues_To_Non_Empty_String_Collection()
        {
            var commaSeparatedValues = "statementiq,llc,rocks,the,market";

            var target = new List<string> {"Miami Project"};
            var countBeforeAdd = target.Count;

            target.AddCommaSeparatedValues(commaSeparatedValues);

            Assert.Equal(target.Count, commaSeparatedValues.Split(',').Length + countBeforeAdd);
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_AddCommaSeparatedValues_With_Empty_String()
        {
            var target = new List<string>();

            Assert.Throws<ArgumentException>(() => target.AddCommaSeparatedValues(string.Empty));
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_AddCommaSeparatedValues_With_Null_String()
        {
            var target = new List<string>();

            Assert.Throws<ArgumentException>(() => target.AddCommaSeparatedValues(null));
        }

        [Fact]
        public void Should_Throw_ArgumentException_When_AddCommaSeparatedValues_With_WhiteSpaces()
        {
            var target = new List<string>();

            Assert.Throws<ArgumentException>(() => target.AddCommaSeparatedValues("  "));
        }
    }
}
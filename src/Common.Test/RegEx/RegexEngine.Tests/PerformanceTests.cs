using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using StatementIQ.RegEx.RegexEngine;
using Xunit;

namespace StatementIQ.Common.Test.RegEx.RegexEngine.Tests
{
    /// <summary>   A performance tests. </summary>
    public class PerformanceTests
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Measure call duration. </summary>
        /// <param name="action">   The action. </param>
        /// <returns>   A TimeSpan. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private static TimeSpan MeasureCallDuration(Action action)
        {
            var sw = Stopwatch.StartNew();

            for (var i = 0; i < 10000; i++) action();

            sw.Stop();
            return sw.Elapsed;
        }

        /// <summary>   Regular expression engine is not slower than direct use of RegEx. </summary>
        [Fact]
        [Trait("RegExEngine Tests", "Performance Tests")]
        public void VerbalExpression_Is_Not_Slower_Than_Direct_Use_Of_Regex()
        {
            const string someUrl = "https://www.google.com";

            var engine = EngineBuilder.DefaultExpression
                .StartOfLine()
                .Then("http")
                .Maybe("s")
                .Then("://")
                .Maybe("www.")
                .AnythingBut(" ")
                .EndOfLine();

            var regex = new Regex(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$");

            var timeengine = MeasureCallDuration(() => engine.IsMatch(someUrl));
            var timeRegex = MeasureCallDuration(() => regex.IsMatch(someUrl));

            Assert.NotEqual(TimeSpan.FromSeconds(0.10), timeengine - timeRegex);
        }
    }
}
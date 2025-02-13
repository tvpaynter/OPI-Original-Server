using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MandateThat;

namespace StatementIQ.RegEx.RegexEngine
{
    /// <summary>
    ///     An engine builder designed to make building and working with Regular Expressions
    ///     much easier than the normal approach.
    /// </summary>
    public class EngineBuilder
    {
        #region Statics

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the default expression (a new instance of the EngineBuilder). </summary>
        /// <value> The default expression. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static EngineBuilder DefaultExpression => new EngineBuilder();

        #endregion Statics

        #region Private Members

        /// <summary>   The RegEx cache. </summary>
        private readonly RegexCache regexCache = new RegexCache();

        /// <summary>   The prefixes. </summary>
        private readonly StringBuilder prefixes = new StringBuilder();

        /// <summary>   Source for the. </summary>
        private readonly StringBuilder source = new StringBuilder();

        /// <summary>   The suffixes. </summary>
        private readonly StringBuilder suffixes = new StringBuilder();

        /// <summary>   The modifiers. </summary>
        private RegexOptions modifiers = RegexOptions.Multiline;

        #endregion Private Members

        #region Private Properties

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets the RegEx string. This creates a new instance of a StringBuilder,
        ///     then appends the prefixes, source and suffixes to create a new string.
        /// </summary>
        /// <value> The RegEx string. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private string RegexString => new StringBuilder().Append(prefixes).Append(source).Append(suffixes).ToString();

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the regex pattern from the cache. </summary>
        /// <value> The pattern RegEx. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private Regex PatternRegex => regexCache.Get(RegexString, modifiers);

        #endregion Private Properties

        #region Public Methods

        #region Helpers

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Escapes a minimal set of metacharacters (\, *, +, ?, |, {, [, (, ), ^, $, ., #, and
        ///     whitespace) by replacing them with their \ codes. This converts a string such that
        ///     it can be used as a constant within a regular expression safely. (Note that the
        ///     reason # and whitespace must be escaped is so the string can be used safely
        ///     within an expression parsed with x mode. If future Regex features add
        ///     additional metacharacters, developers should depend on Escape to escape those
        ///     characters as well.)
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are
        ///     null.
        /// </exception>
        /// <param name="value">    The value. </param>
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public string Sanitize(string value)
        {
            Mandate.That(value, nameof(value)).IsNotNull();
            return Regex.Escape(value ?? string.Empty);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Tests to see if the supplied string is a match. </summary>
        /// <param name="toTest">   to test. </param>
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Test(string toTest)
        {
            //           Mandate.That(toTest, nameof(toTest)).IsNotNullOrWhiteSpace();
            return IsMatch(toTest);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Query if 'toTest' is match. </summary>
        /// <param name="toTest">   to test. </param>
        /// <returns>   True if match, false if not. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool IsMatch(string toTest)
        {
            Mandate.That(toTest, nameof(toTest)).IsNotNull();
            return PatternRegex.IsMatch(toTest);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Converts this object to a regular expression. </summary>
        /// <returns>   This object as a RegEx. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public Regex ToRegex()
        {
            return PatternRegex;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Returns a string that represents the current object. </summary>
        /// <returns>   A string that represents the current object. </returns>
        /// <seealso cref="M:System.Object.ToString()" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public override string ToString()
        {
            return PatternRegex.ToString();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Captures a collection of groups by the toText Match expression. The groupName
        ///     parameter will be used for the match groups to return the correct value.
        /// </summary>
        /// <param name="toTest">       to test. </param>
        /// <param name="groupName">    Name of the group. </param>
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public string Capture(string toTest, string groupName)
        {
            if (!Test(toTest ?? string.Empty)) return null;

            var match = PatternRegex.Match(toTest ?? string.Empty);
            return match.Groups[groupName].Value;
        }

        #endregion Helpers

        #region Expression Modifiers

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds value. The string IS NOT SANITIZED with this function </summary>
        /// <param name="commonRegex">  The common RegEx. </param>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder Add(CommonRegex commonRegex)
        {
            return Add(commonRegex.Name, false);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds value. By default, the string supplied is sanitized. </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are
        ///     null.
        /// </exception>
        /// <param name="value">    The value. </param>
        /// <param name="sanitize"> (Optional) True to sanitize. </param>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder Add(string value, bool sanitize = true)
        {
            Mandate.That(value, nameof(value)).IsNotNullOrWhiteSpace();

            value = sanitize ? Sanitize(value) : value;
            source.Append(value);
            return this;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Appends the 'start of line' character (^). </summary>
        /// <param name="enable">   (Optional) True to enable the character, false to disable (supplies empty string). </param>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder StartOfLine(bool enable = true)
        {
            prefixes.Append(enable ? "^" : string.Empty);
            return this;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Appends the 'end of line' character ($). </summary>
        /// <param name="enable">   (Optional) True to enable the character, false to disable (supplies empty string). </param>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder EndOfLine(bool enable = true)
        {
            suffixes.Append(enable ? "$" : string.Empty);
            return this;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Matches the pattern inside and capture its match. </summary>
        /// <param name="value">    The value. </param>
        /// <param name="sanitize"> (Optional) True to sanitize the string, otherwise supply the default value. </param>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder Then(string value, bool sanitize = true)
        {
            var sanitizedValue = sanitize ? Sanitize(value) : value;
            value = $"({sanitizedValue})";
            return Add(value, false);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Matches the pattern inside and capture its match. </summary>
        /// <param name="commonRegex">  The common RegEx expression to use. </param>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder Then(CommonRegex commonRegex)
        {
            Mandate.That(commonRegex, nameof(commonRegex)).IsNotNull();
            return Then(commonRegex?.Name, false);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Searches for the first match for the given string. </summary>
        /// <param name="value">    The value. </param>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder Find(string value)
        {
            Mandate.That(value, nameof(value)).IsNotNullOrWhiteSpace();
            return Then(value);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Appends the value to the expression. </summary>
        /// <param name="value">    The value. </param>
        /// <param name="sanitize"> (Optional) True to sanitize the string, otherwise supply the default value. </param>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder Maybe(string value, bool sanitize = true)
        {
            Mandate.That(value, nameof(value)).IsNotNullOrWhiteSpace();
            value = sanitize ? Sanitize(value) : value;
            value = $"({value})?";
            return Add(value, false);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Appends the value to the expression. </summary>
        /// <param name="commonRegex">  The common RegEx expression to use. </param>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder Maybe(CommonRegex commonRegex)
        {
            Mandate.That(commonRegex, nameof(commonRegex)).IsNotNull();
            return Maybe(commonRegex?.Name, false);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Match the pattern inside and capture its match.
        ///     Match any single character 0 or more times.
        /// </summary>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder Anything()
        {
            return Add("(.*)", false);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Returns anything but the supplied expression. </summary>
        /// <param name="value">    The value. </param>
        /// <param name="sanitize"> (Optional) True to sanitize the string, otherwise supply the default value. </param>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder AnythingBut(string value, bool sanitize = true)
        {
            Mandate.That(value, nameof(value)).IsNotNull();
            value = sanitize ? Sanitize(value) : value;
            value = $"([^{value}]*)";
            return Add(value, false);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Matches the pattern inside and capture its match. Matches any single character
        ///     1 or more times
        /// </summary>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder Something()
        {
            return Add("(.+)", false);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Matches anything BUT the pattern inside and capture the resultant match. </summary>
        /// <param name="value">    The value. </param>
        /// <param name="sanitize"> (Optional) True to sanitize. </param>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder SomethingBut(string value, bool sanitize = true)
        {
            Mandate.That(value, nameof(value)).IsNotNullOrWhiteSpace();

            value = sanitize ? Sanitize(value) : value;
            value = $"([^{value}]+)";
            return Add(value, false);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>Replaces all occurrences of a specified string in this instance with another specified string.</summary>
        /// <param name="value">    The value. </param>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder Replace(string value)
        {
            //Mandate.That(value, nameof(value)).IsNotNullOrWhiteSpace();

            var whereToReplace = PatternRegex.ToString();
            if (whereToReplace.Length != 0) source.Replace(whereToReplace, value ?? string.Empty);

            return this;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Matches the pattern inside and captures its match. Match a new line(line feed)
        ///     or match the pattern inside and capture its match. Match a carriage return and also
        ///     match a new line(line feed).
        /// </summary>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder LineBreak()
        {
            return Add(@"(\n|(\r\n))", false);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds a line break. </summary>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder Br()
        {
            return LineBreak();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds a tab character (\t). </summary>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder Tab()
        {
            return Add(@"\t");
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds a word character (\w+). </summary>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder Word()
        {
            return Add(@"\w+", false);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates an expression which will search for any of the supplied 'value'. </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are
        ///     null.
        /// </exception>
        /// <param name="value">    The value. </param>
        /// <param name="sanitize"> (Optional) True to sanitize the string, otherwise, use the supplied value. </param>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder AnyOf(string value, bool sanitize = true)
        {
            Mandate.That(value, nameof(value)).IsNotNullOrWhiteSpace();

            value = sanitize ? Sanitize(value) : value;
            value = $"[{value}]";
            return Add(value, false);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Returns any character in the supplied string. </summary>
        /// <param name="value">    The value. </param>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder Any(string value)
        {
            return AnyOf(value);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates an expression given a range of arguments. </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments
        ///     are null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when one or more arguments are outside
        ///     the required range.
        /// </exception>
        /// <param name="arguments">    A variable-length parameters list containing arguments. </param>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder Range(params object[] arguments)
        {
            Mandate.That(arguments, nameof(arguments)).IsNotNull();
            if (arguments?.Length <= 1)
                throw new ArgumentOutOfRangeException(nameof(arguments) + " must contain at least 1 parameter");
            //Mandate.That(arguments, nameof(arguments)).IsGreaterThan(1);

            var sanitizedStrings = arguments?.Select(argument =>
                {
                    if (argument is null) return string.Empty;

                    var casted = argument.ToString();
                    return string.IsNullOrWhiteSpace(casted) ? string.Empty : Sanitize(casted);
                })
                .Where(sanitizedString => !string.IsNullOrWhiteSpace(sanitizedString))
                .OrderBy(s => s)
                .ToArray();

            if (sanitizedStrings?.Length > 3) throw new ArgumentOutOfRangeException(nameof(arguments));

            var any = (sanitizedStrings ?? Array.Empty<string>()).Length > 0;
            if (!any) return this;

            var hasOddNumberOfParams = sanitizedStrings?.Length % 2 > 0;

            var sb = new StringBuilder("[");
            for (var from = 0; from < sanitizedStrings?.Length; from += 2)
            {
                var to = from + 1;
                if (sanitizedStrings.Length <= to) break;

                _ = sb.AppendFormat("{0}-{1}", sanitizedStrings[from], sanitizedStrings[to]);
            }

            sb.Append("]");

            if (hasOddNumberOfParams) _ = sb.AppendFormat("|{0}", sanitizedStrings[sanitizedStrings.Length - 1]);

            return Add(sb.ToString(), false);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Matches the pattern inside and captures its match. Matches the characters
        ///     supplied literally 1 or more times.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are
        ///     null.
        /// </exception>
        /// <param name="value">    The value. </param>
        /// <param name="sanitize"> (Optional) True to sanitize. </param>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder Multiple(string value, bool sanitize = true)
        {
            Mandate.That(value, nameof(value)).IsNotNullOrWhiteSpace();

            value = sanitize ? Sanitize(value) : value;
            value = $"({value})+";
            return Add(value, false);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Creates a regular expression which uses the supplied value and appends it to the
        ///     current prefixes and suffixes, creating a condition to where the evaluation is
        ///     between 2 strings.
        /// </summary>
        /// <param name="commonRegex">  The common RegEx expression to use. </param>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder Or(CommonRegex commonRegex)
        {
            Mandate.That(commonRegex, nameof(commonRegex)).IsNotNull();
            return Or(commonRegex?.Name, false);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Creates a regular expression which uses the supplied value and appends it to the
        ///     current prefixes and suffixes, creating a condition to where the evaluation is
        ///     between 2 strings.
        /// </summary>
        /// <param name="value">    The value. </param>
        /// <param name="sanitize"> (Optional) True to sanitize. </param>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder Or(string value, bool sanitize = true)
        {
            Mandate.That(value, nameof(value)).IsNotNullOrWhiteSpace();

            prefixes.Append('(');
            suffixes.Insert(0, ')');
            source.Append(")|(");
            return Add(value, sanitize);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds a 'begin capture' statement to the regular expression ('('). </summary>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder BeginCapture()
        {
            return Add("(", false);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Adds a 'begin capture' statement to the regular expression ('('). In doing so,
        ///     it creates an expression that adds a group name to the capture.
        /// </summary>
        /// <param name="groupName">    Name of the group. </param>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder BeginCapture(string groupName)
        {
            return Add("(?<", false).Add(groupName).Add(">", false);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds the 'end capture' statement to the regular expression (')'). </summary>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder EndCapture()
        {
            return Add(")", false);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Repeat the previous command 'n' times. </summary>
        /// <param name="n">    The number of times to repeat. </param>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder RepeatPrevious(int n)
        {
            return Add("{" + n + "}", false);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Repeat at least 'n' times, but no more than 'm' times. </summary>
        /// <param name="n">    The minimum number of times to repeat. </param>
        /// <param name="m">    The maximum number of times to repeat (no more than). </param>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder RepeatPrevious(int n, int m)
        {
            return Add("{" + n + "," + m + "}", false);
        }

        #endregion Expression Modifiers

        #region Expression Options Modifiers

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds a modifier to the regex expression. </summary>
        /// <param name="modifier"> The modifier. </param>
        /// <remarks>
        ///     i - Ignore Case
        ///     x - Ignore Pattern WhiteSpace
        ///     m - Multiline
        ///     s - Single line
        /// </remarks>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder AddModifier(char modifier)
        {
            switch (modifier)
            {
                case 'i':
                    modifiers |= RegexOptions.IgnoreCase;
                    break;
                case 'x':
                    modifiers |= RegexOptions.IgnorePatternWhitespace;
                    break;
                case 'm':
                    modifiers |= RegexOptions.Multiline;
                    break;
                case 's':
                    modifiers |= RegexOptions.Singleline;
                    break;
            }

            return this;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Removes the modifier described by the supplied modifier. </summary>
        /// <param name="modifier"> The modifier. </param>
        /// <remarks>
        ///     i - Ignore Case
        ///     x - Ignore Pattern WhiteSpace
        ///     m - Multiline
        ///     s - Single line
        /// </remarks>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder RemoveModifier(char modifier)
        {
            switch (modifier)
            {
                case 'i':
                    modifiers &= ~RegexOptions.IgnoreCase;
                    break;
                case 'x':
                    modifiers &= ~RegexOptions.IgnorePatternWhitespace;
                    break;
                case 'm':
                    modifiers &= ~RegexOptions.Multiline;
                    break;
                case 's':
                    modifiers &= ~RegexOptions.Singleline;
                    break;
            }

            return this;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds the 'IgnoreCase' flag (i) to enable case sensitivity, otherwise remove it. </summary>
        /// <param name="enable">   (Optional) True to enable/add, false to disable. </param>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder WithAnyCase(bool enable = true)
        {
            if (enable)
                AddModifier('i');
            else
                RemoveModifier('i');
            return this;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds the parameter to enable single line searching only ('m'). </summary>
        /// <param name="enable">   True to enable/add, false to disable/remove. </param>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder UseOneLineSearchOption(bool enable)
        {
            if (enable)
                RemoveModifier('m');
            else
                AddModifier('m');

            return this;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds the supplied options to the expression. </summary>
        /// <param name="options">  Options for controlling the operation. </param>
        /// <returns>   The EngineBuilder. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public EngineBuilder WithOptions(RegexOptions options)
        {
            modifiers = options;
            return this;
        }

        #endregion Expression Options Modifiers

        #endregion Public Methods
    }
}
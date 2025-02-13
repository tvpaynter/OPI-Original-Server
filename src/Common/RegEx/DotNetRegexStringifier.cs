using System;

namespace StatementIQ.RegEx
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A dot net RegEx stringifier. This class cannot be inherited. </summary>
    /// <remarks>   StatementIQ, 5/14/2020. </remarks>
    /// <seealso cref="T:StatementIQ.RegEx.RegexStringifier" />
    /// <seealso cref="T:StatementIQ.RegEx.IMultipleNamedCaptureGroupSyntaxRegexStringifier" />
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public sealed class DotNetRegexStringifier : RegexStringifier, IMultipleNamedCaptureGroupSyntaxRegexStringifier
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the RegEx language. </summary>
        /// <value> The RegEx language. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override RegexLanguage RegexLanguage => RegexLanguage.DotNet;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets a value indicating whether this is a named capturing group supported.
        /// </summary>
        /// <value> True if this  is named capturing group supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override bool IsNamedCapturingGroupSupported => true;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets a value indicating whether inline group options are supported.
        /// </summary>
        /// <value> True if this  is inline group options supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override bool IsInlineGroupOptionsSupported => true;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a value indicating whether atomic group option is supported. </summary>
        /// <value> True if this  is atomic group supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override bool IsAtomicGroupSupported => true;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets a value indicating whether a positive look-ahead assertion is supported.
        /// </summary>
        /// <value> True if this  is positive look-ahead assertion supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override bool IsPositiveLookaheadAssertionSupported => true;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets a value indicating whether a negative look-ahead assertion is supported.
        /// </summary>
        /// <value> True if this  is negative look-ahead assertion supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override bool IsNegativeLookaheadAssertionSupported => true;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets a value indicating whether this a positive look-behind assertion is supported.
        /// </summary>
        /// <value> True if this  is positive look-behind assertion supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override bool IsPositiveLookbehindAssertionSupported => true;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets a value indicating whether a negative look-behind assertion is supported.
        /// </summary>
        /// <value> True if this  is negative look-behind assertion supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override bool IsNegativeLookbehindAssertionSupported => true;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a value indicating whether a unicode category is supported. </summary>
        /// <value> True if this  is unicode category supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override bool IsUnicodeCategorySupported => true;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a value indicating whether a conditional option is supported. </summary>
        /// <value> True if this  is conditional supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override bool IsConditionalSupported => true;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a value indicating whether this supports angle bracket naming. </summary>
        /// <value> True if supports angle bracket naming, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool SupportsAngleBracketNaming => true;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a value indicating whether this supports apostrophe naming. </summary>
        /// <value> True if supports apostrophe naming, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool SupportsApostropheNaming => true;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the named capture group bracket option. </summary>
        /// <value> The named capture group bracket option. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public NamedCaptureGroupBracketOption NamedCaptureGroupBracketOption { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Converts a token to a token string. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when one or more arguments are outside
        ///     the required range.
        /// </exception>
        /// <param name="token">    The token. </param>
        /// <returns>   Token as a string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public override string ToTokenString(RegexToken token)
        {
            switch (token)
            {
                case RegexToken.GroupNameOpen:
                    return NamedCaptureGroupBracketOption switch
                    {
                        NamedCaptureGroupBracketOption.AngleBracket => "<",
                        NamedCaptureGroupBracketOption.Apostrophe => "'",
                        _ => throw new ArgumentOutOfRangeException(nameof(NamedCaptureGroupBracketOption))
                    };

                case RegexToken.GroupNameClose:
                    return NamedCaptureGroupBracketOption switch
                    {
                        NamedCaptureGroupBracketOption.AngleBracket => ">",
                        NamedCaptureGroupBracketOption.Apostrophe => "'",
                        _ => throw new ArgumentOutOfRangeException(nameof(NamedCaptureGroupBracketOption))
                    };
                default:
                    return base.ToTokenString(token);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Converts a group to a group string. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="NotSupportedException">
        ///     Thrown when the requested operation is not
        ///     supported.
        /// </exception>
        /// <param name="group">    The group. </param>
        /// <returns>   Group as a string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public override string ToGroupString(RegexGroupNode group)
        {
            if (!string.IsNullOrWhiteSpace(group.Name) && !string.IsNullOrWhiteSpace(group.Options))
                throw new NotSupportedException(
                    $"Due to a constraint from dotnet, the properties {nameof(group.Name)} and {nameof(group.Options)} of {nameof(RegexGroupNode)} cannot be set at the same time.");

            return base.ToGroupString(group);
        }
    }
}
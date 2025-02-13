using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using MandateThat;
using StatementIQ.RegEx.Exceptions;

namespace StatementIQ.RegEx
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     A class which represents a RegEx stringifier, which provides the ability to represent
    ///     these classes in a human-readable manner.
    /// </summary>
    /// <remarks>   StatementIQ, 5/14/2020. </remarks>
    /// <seealso cref="T:StatementIQ.RegEx.IRegexStringifier" />
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public abstract class RegexStringifier : IRegexStringifier
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the RegEx language. </summary>
        /// <value> The RegEx language. </value>
        /// <seealso cref="P:StatementIQ.RegEx.IRegexStringifier.RegexLanguage" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public abstract RegexLanguage RegexLanguage { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets a value indicating whether the named capturing group option is supported.
        /// </summary>
        /// <value> True if the named capturing group is supported, false if not. </value>
        /// <seealso cref="P:StatementIQ.RegEx.IRegexStringifier.IsNamedCapturingGroupSupported" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public abstract bool IsNamedCapturingGroupSupported { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a value indicating whether the inline group options are supported. </summary>
        /// <value> True if the inline group options are supported, false if not. </value>
        /// <seealso cref="P:StatementIQ.RegEx.IRegexStringifier.IsInlineGroupOptionsSupported" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public abstract bool IsInlineGroupOptionsSupported { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a value indicating whether the atomic group option is supported. </summary>
        /// <value> True if the atomic group is supported, false if not. </value>
        /// <seealso cref="P:StatementIQ.RegEx.IRegexStringifier.IsAtomicGroupSupported" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public abstract bool IsAtomicGroupSupported { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets a value indicating whether the positive look-ahead assertion option is supported.
        /// </summary>
        /// <value> True if the positive look-ahead assertion is supported, false if not. </value>
        /// <seealso cref="P:StatementIQ.RegEx.IRegexStringifier.IsPositiveLookaheadAssertionSupported" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public abstract bool IsPositiveLookaheadAssertionSupported { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets a value indicating whether the negative look-ahead assertion option is supported.
        /// </summary>
        /// <value> True if the negative look-ahead assertion is supported, false if not. </value>
        /// <seealso cref="P:StatementIQ.RegEx.IRegexStringifier.IsNegativeLookaheadAssertionSupported" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public abstract bool IsNegativeLookaheadAssertionSupported { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets a value indicating whether the positive look-behind assertion option is supported.
        /// </summary>
        /// <value> True if the positive look-behind assertion is supported, false if not. </value>
        /// <seealso cref="P:StatementIQ.RegEx.IRegexStringifier.IsPositiveLookbehindAssertionSupported" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public abstract bool IsPositiveLookbehindAssertionSupported { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets a value indicating whether the negative look-behind assertion option is supported.
        /// </summary>
        /// <value> True if the negative look-behind assertion is supported, false if not. </value>
        /// <seealso cref="P:StatementIQ.RegEx.IRegexStringifier.IsNegativeLookbehindAssertionSupported" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public abstract bool IsNegativeLookbehindAssertionSupported { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a value indicating whether the unicode category option is supported. </summary>
        /// <value> True if the unicode category is supported, false if not. </value>
        /// <seealso cref="P:StatementIQ.RegEx.IRegexStringifier.IsUnicodeCategorySupported" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public abstract bool IsUnicodeCategorySupported { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a value indicating whether the conditional option is supported. </summary>
        /// <value> True if the conditional option is supported, false if not. </value>
        /// <seealso cref="P:StatementIQ.RegEx.IRegexStringifier.IsConditionalSupported" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public abstract bool IsConditionalSupported { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Convert this into a string representation. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are
        ///     null.
        /// </exception>
        /// <param name="node"> The node. </param>
        /// <returns>   A string that represents this. </returns>
        /// <seealso cref="M:StatementIQ.Common.Library.RegEx.IRegexStringifier.ToString(RegexNode)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual string ToString(RegexNode node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            switch (node)
            {
                case RegexAtomicGroupNode ag:
                    return ToAtomicGroupString(ag);
                case RegexConditionalNode c:
                    return ToConditionalString(c);
                case RegexPositiveLookaheadAssertionNode pla:
                    return ToPositiveLookaheadAssertionString(pla);
                case RegexNegativeLookaheadAssertionNode nla:
                    return ToNegativeLookaheadAssertionString(nla);
                case RegexPositiveLookbehindAssertionNode plb:
                    return ToPositiveLookbehindAssertionString(plb);
                case RegexNegativeLookbehindAssertionNode nlb:
                    return ToNegativeLookbehindAssertionString(nlb);
                case RegexGroupNode g:
                    return ToGroupString(g);
                case RegexUnicodeCategoryNode un:
                    return ToUnicodeCategoryString(un);
                default:
                    return ToNodeString(node);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds a quantifier. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when one or more arguments are outside
        ///     the required range.
        /// </exception>
        /// <param name="pattern">          Specifies the pattern. </param>
        /// <param name="min">              The minimum value. </param>
        /// <param name="max">              The maximum value. </param>
        /// <param name="quantifierOption"> The quantifier option. </param>
        /// <returns>   A string. </returns>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.IRegexStringifier.AddQuantifier(string,int?,int?,RegexQuantifierOption)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual string AddQuantifier(string pattern, int? min, int? max, RegexQuantifierOption quantifierOption)
        {
            if (min.HasValue) Mandate.That(min.Value >= 0, nameof(min)).IsTrue();

            if (max.HasValue) Mandate.That(max.Value >= 0, nameof(max)).IsTrue();

            var quantifierOptionString = ToQuantifierOptionString(quantifierOption);
            var result = string.Empty;

            if (!min.HasValue && !max.HasValue || min == 1 && max == 1)
            {
                result = pattern;
            }
            else if (min.HasValue && max.HasValue)
            {
                if (min == 0 && max == 1)
                    result = pattern + ToTokenString(RegexToken.ZeroOrOne);
                else if (min == max)
                    result =
                        $"{pattern}{ToTokenString(RegexToken.QuantifierOpen)}{min.Value}{ToTokenString(RegexToken.QuantifierClose)}";
                else
                    result =
                        $"{pattern}{ToTokenString(RegexToken.QuantifierOpen)}{min.Value}{ToTokenString(RegexToken.QuantifierSeparator)}{max.Value}{ToTokenString(RegexToken.QuantifierClose)}";
            }
            else if (min == 0 && !max.HasValue)
            {
                result = pattern + ToTokenString(RegexToken.ZeroOrMore);
            }
            else if (min == 1 && !max.HasValue)
            {
                result = pattern + ToTokenString(RegexToken.OneOrMore);
            }
            else if (min.HasValue && !max.HasValue)
            {
                result =
                    $"{pattern}{ToTokenString(RegexToken.QuantifierOpen)}{min.Value}{ToTokenString(RegexToken.QuantifierSeparator)}{ToTokenString(RegexToken.QuantifierClose)}";
            }
            else if (!min.HasValue && max.HasValue)
            {
                result =
                    $"{pattern}{ToTokenString(RegexToken.QuantifierOpen)}{ToTokenString(RegexToken.QuantifierSeparator)}{max.Value}{ToTokenString(RegexToken.QuantifierClose)}";
            }
            else
            {
                throw new NotImplementedException();
            }

            return result + quantifierOptionString;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Converts a quantifierOption to a quantifier option string. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when one or more arguments are outside
        ///     the required range.
        /// </exception>
        /// <param name="quantifierOption"> The quantifier option. </param>
        /// <returns>   QuantifierOption as a string. </returns>
        /// <seealso cref="M:StatementIQ.Common.Library.RegEx.IRegexStringifier.ToQuantifierOptionString(RegexQuantifierOption)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual string ToQuantifierOptionString(RegexQuantifierOption quantifierOption)
        {
            switch (quantifierOption)
            {
                case RegexQuantifierOption.Greedy:
                    return string.Empty;
                case RegexQuantifierOption.Lazy:
                    return ToTokenString(RegexToken.Lazy);
                case RegexQuantifierOption.Possessive:
                    return ToTokenString(RegexToken.Possessive);
                default:
                    throw new ArgumentOutOfRangeException(nameof(quantifierOption));
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Converts a token to an actual RegEx expression. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="InlineGroupOptionsNotSupportedException">
        ///     Thrown when an Inline
        ///     Group Options Not
        ///     Supported error condition
        ///     occurs.
        /// </exception>
        /// <exception cref="NamedCapturingGroupNotSupportedException">
        ///     Thrown when a Named
        ///     Capturing Group Not
        ///     Supported error condition
        ///     occurs.
        /// </exception>
        /// <exception cref="ConditionalNotSupportedException">
        ///     Thrown when a Conditional
        ///     Not Supported error
        ///     condition occurs.
        /// </exception>
        /// <exception cref="AtomicGroupNotSupportedException">
        ///     Thrown when an Atomic
        ///     Group Not Supported error
        ///     condition occurs.
        /// </exception>
        /// <exception cref="PositiveLookaheadAssertionNotSupportedException">
        ///     Thrown when a Positive
        ///     Look-ahead Assertion Not
        ///     Supported error condition
        ///     occurs.
        /// </exception>
        /// <exception cref="NegativeLookaheadAssertionNotSupportedException">
        ///     Thrown when a Negative
        ///     Look-ahead Assertion Not
        ///     Supported error condition
        ///     occurs.
        /// </exception>
        /// <exception cref="PositiveLookbehindAssertionNotSupportedException">
        ///     Thrown when a Positive
        ///     Look-behind Assertion Not
        ///     Supported error condition
        ///     occurs.
        /// </exception>
        /// <exception cref="NegativeLookbehindAssertionNotSupportedException">
        ///     Thrown when a Negative
        ///     Look-behind Assertion Not
        ///     Supported error condition
        ///     occurs.
        /// </exception>
        /// <exception cref="UnicodeCategoryNotSupportedException">
        ///     Thrown when an Unicode
        ///     Category Not Supported
        ///     error condition occurs.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when one or more
        ///     arguments are outside the
        ///     required range.
        /// </exception>
        /// <param name="token">    The token. </param>
        /// <returns>   Token as a string. </returns>
        /// <seealso cref="M:StatementIQ.Common.Library.RegEx.IRegexStringifier.ToTokenString(RegexToken)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual string ToTokenString(RegexToken token)
        {
            switch (token)
            {
                case RegexToken.Any:
                    return ".";
                case RegexToken.GroupOpen:
                    return "(";
                case RegexToken.GroupNonCapturing:
                    return "?:";
                case RegexToken.GroupOptionStart:
                    if (!IsInlineGroupOptionsSupported)
                        throw new InlineGroupOptionsNotSupportedException(RegexLanguage);

                    return "?";
                case RegexToken.GroupNameOpen:
                    if (!IsNamedCapturingGroupSupported)
                        throw new NamedCapturingGroupNotSupportedException(RegexLanguage);

                    return "<";
                case RegexToken.GroupNameClose:
                    if (!IsNamedCapturingGroupSupported)
                        throw new NamedCapturingGroupNotSupportedException(RegexLanguage);

                    return ">";
                case RegexToken.GroupOptionEnd:
                    if (!IsInlineGroupOptionsSupported)
                        throw new InlineGroupOptionsNotSupportedException(RegexLanguage);

                    return ":";
                case RegexToken.GroupClose:
                    return ")";
                case RegexToken.ConditionalOpen:
                    if (!IsConditionalSupported) throw new ConditionalNotSupportedException(RegexLanguage);

                    return "(?";
                case RegexToken.ConditionalClose:
                    if (!IsConditionalSupported) throw new ConditionalNotSupportedException(RegexLanguage);

                    return ")";
                case RegexToken.AtomicGroupOpen:
                    if (!IsAtomicGroupSupported) throw new AtomicGroupNotSupportedException(RegexLanguage);

                    return "(?>";
                case RegexToken.AtomicGroupClose:
                    if (!IsAtomicGroupSupported) throw new AtomicGroupNotSupportedException(RegexLanguage);

                    return ")";
                case RegexToken.PositiveLookaheadAssertionOpen:
                    if (!IsPositiveLookaheadAssertionSupported)
                        throw new PositiveLookaheadAssertionNotSupportedException(RegexLanguage);

                    return "(?=";
                case RegexToken.PositiveLookaheadAssertionClose:
                    if (!IsPositiveLookaheadAssertionSupported)
                        throw new PositiveLookaheadAssertionNotSupportedException(RegexLanguage);

                    return ")";
                case RegexToken.NegativeLookaheadAssertionOpen:
                    if (!IsNegativeLookaheadAssertionSupported)
                        throw new NegativeLookaheadAssertionNotSupportedException(RegexLanguage);

                    return "(?!";
                case RegexToken.NegativeLookaheadAssertionClose:
                    if (!IsNegativeLookaheadAssertionSupported)
                        throw new NegativeLookaheadAssertionNotSupportedException(RegexLanguage);

                    return ")";
                case RegexToken.PositiveLookbehindAssertionOpen:
                    if (!IsPositiveLookbehindAssertionSupported)
                        throw new PositiveLookbehindAssertionNotSupportedException(RegexLanguage);

                    return "(?<=";
                case RegexToken.PositiveLookbehindAssertionClose:
                    if (!IsPositiveLookbehindAssertionSupported)
                        throw new PositiveLookbehindAssertionNotSupportedException(RegexLanguage);

                    return ")";
                case RegexToken.NegativeLookbehindAssertionOpen:
                    if (!IsNegativeLookbehindAssertionSupported)
                        throw new NegativeLookbehindAssertionNotSupportedException(RegexLanguage);

                    return "(?<!";
                case RegexToken.NegativeLookbehindAssertionClose:
                    if (!IsNegativeLookbehindAssertionSupported)
                        throw new NegativeLookbehindAssertionNotSupportedException(RegexLanguage);

                    return ")";
                case RegexToken.CharacterGroupOpen:
                    return "[";
                case RegexToken.CharacterGroupNegative:
                    return "^";
                case RegexToken.CharacterGroupClose:
                    return "]";
                case RegexToken.Alternation:
                    return "|";
                case RegexToken.ZeroOrOne:
                    return "?";
                case RegexToken.ZeroOrMore:
                    return "*";
                case RegexToken.OneOrMore:
                    return "+";
                case RegexToken.Lazy:
                    return "?";
                case RegexToken.Possessive:
                    return "+";
                case RegexToken.QuantifierOpen:
                    return "{";
                case RegexToken.QuantifierSeparator:
                    return ",";
                case RegexToken.QuantifierClose:
                    return "}";
                case RegexToken.StringStartAnchor:
                    return "^";
                case RegexToken.StringEndAnchor:
                    return "$";
                case RegexToken.UnicodeCategoryOpen:
                    if (!IsUnicodeCategorySupported) throw new UnicodeCategoryNotSupportedException(RegexLanguage);

                    return @"\p{";
                case RegexToken.UnicodeCategoryClose:
                    if (!IsUnicodeCategorySupported) throw new UnicodeCategoryNotSupportedException(RegexLanguage);

                    return "}";
                case RegexToken.NegativeUnicodeCategoryOpen:
                    if (!IsUnicodeCategorySupported) throw new UnicodeCategoryNotSupportedException(RegexLanguage);

                    return @"\P{";
                case RegexToken.NegativeUnicodeCategoryClose:
                    if (!IsUnicodeCategorySupported) throw new UnicodeCategoryNotSupportedException(RegexLanguage);

                    return "}";
                default:
                    throw new ArgumentOutOfRangeException(nameof(token), "Argument is not supported");
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Converts a node to a node string. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are
        ///     null.
        /// </exception>
        /// <param name="node"> The node. </param>
        /// <returns>   Node as a string. </returns>
        /// <seealso cref="M:StatementIQ.Common.Library.RegEx.IRegexStringifier.ToNodeString(RegexNode)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual string ToNodeString(RegexNode node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            return node.IsInnerNodeIncluded
                ? AddQuantifier(ToString(node.InnerNode), node.Minimum, node.Maximum, node.RegexQuantifierOption)
                : AddQuantifier(node.Pattern, node.Minimum, node.Maximum, node.RegexQuantifierOption);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Converts a group to a group string. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="NamedCapturingGroupNotSupportedException">
        ///     Thrown when a Named Capturing
        ///     Group Not Supported error
        ///     condition occurs.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required
        ///     arguments are null.
        /// </exception>
        /// <param name="group">    The group. </param>
        /// <returns>   Group as a string. </returns>
        /// <seealso cref="M:StatementIQ.Common.Library.RegEx.IRegexStringifier.ToGroupString(RegexGroupNode)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual string ToGroupString(RegexGroupNode group)
        {
            if (!string.IsNullOrWhiteSpace(group.Name) && !IsNamedCapturingGroupSupported)
                throw new NamedCapturingGroupNotSupportedException(RegexLanguage);

            if (group == null) throw new ArgumentNullException(nameof(group));

            var sb = new StringBuilder();

            sb.Append(ToTokenString(RegexToken.GroupOpen));
            if (!string.IsNullOrWhiteSpace(group.Name))
                sb.Append(ToTokenString(RegexToken.GroupOptionStart))
                    .Append(ToTokenString(RegexToken.GroupNameOpen))
                    .Append(group.Name)
                    .Append(ToTokenString(RegexToken.GroupNameClose));
            else if (!string.IsNullOrWhiteSpace(group.Options))
                sb.Append(ToTokenString(RegexToken.GroupOptionStart))
                    .Append(group.Options)
                    .Append(ToTokenString(RegexToken.GroupOptionEnd));
            else if (!group.IsCapturingGroup) sb.Append(ToTokenString(RegexToken.GroupNonCapturing));

            sb.Append(group.IsInnerNodeIncluded ? ToString(group.InnerNode) : group.Pattern)
                .Append(ToTokenString(RegexToken.GroupClose));

            return AddQuantifier(sb.ToString(), group.Minimum, group.Maximum, group.RegexQuantifierOption);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Converts an atomicGroup to an atomic group string. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="AtomicGroupNotSupportedException">
        ///     Thrown when an Atomic Group Not Supported
        ///     error condition occurs.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required
        ///     arguments are null.
        /// </exception>
        /// <param name="atomicGroup">  Group the atomic belongs to. </param>
        /// <returns>   AtomicGroup as a string. </returns>
        /// <seealso cref="M:StatementIQ.Common.Library.RegEx.IRegexStringifier.ToAtomicGroupString(RegexAtomicGroupNode)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual string ToAtomicGroupString(RegexAtomicGroupNode atomicGroup)
        {
            if (!IsAtomicGroupSupported) throw new AtomicGroupNotSupportedException(RegexLanguage);

            if (atomicGroup == null) throw new ArgumentNullException(nameof(atomicGroup));

            var sb = new StringBuilder();

            sb.Append(ToTokenString(RegexToken.AtomicGroupOpen))
                .Append(atomicGroup.IsInnerNodeIncluded ? ToString(atomicGroup.InnerNode) : atomicGroup.Pattern)
                .Append(ToTokenString(RegexToken.AtomicGroupClose));

            return AddQuantifier(
                sb.ToString(),
                atomicGroup.Minimum,
                atomicGroup.Maximum,
                atomicGroup.RegexQuantifierOption);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Converts a positiveLookaheadAssertion to a positive look-ahead assertion string.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="PositiveLookaheadAssertionNotSupportedException">
        ///     Thrown when a Positive
        ///     Look-ahead Assertion Not
        ///     Supported error condition
        ///     occurs.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more
        ///     required arguments are
        ///     null.
        /// </exception>
        /// <param name="positiveLookaheadAssertion">   The positive look-ahead assertion. </param>
        /// <returns>   PositiveLookaheadAssertion as a string. </returns>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.IRegexStringifier.ToPositiveLookaheadAssertionString(RegexPositiveLookaheadAssertionNode)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly",
            Justification = "Reviewed. Suppression is OK here.")]
        public virtual string ToPositiveLookaheadAssertionString(
            RegexPositiveLookaheadAssertionNode positiveLookaheadAssertion)
        {
            if (!IsPositiveLookaheadAssertionSupported)
                throw new PositiveLookaheadAssertionNotSupportedException(RegexLanguage);

            if (positiveLookaheadAssertion == null) throw new ArgumentNullException(nameof(positiveLookaheadAssertion));

            var sb = new StringBuilder();

            sb.Append(ToTokenString(RegexToken.PositiveLookaheadAssertionOpen))
                .Append(positiveLookaheadAssertion.IsInnerNodeIncluded
                    ? ToString(positiveLookaheadAssertion.InnerNode)
                    : positiveLookaheadAssertion.Pattern)
                .Append(ToTokenString(RegexToken.PositiveLookaheadAssertionClose));

            return AddQuantifier(
                sb.ToString(),
                positiveLookaheadAssertion.Minimum,
                positiveLookaheadAssertion.Maximum,
                positiveLookaheadAssertion.RegexQuantifierOption);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Converts a negativeLookaheadAssertion to a negative look-ahead assertion string.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="NegativeLookaheadAssertionNotSupportedException">
        ///     Thrown when a Negative
        ///     Look-ahead Assertion Not
        ///     Supported error condition
        ///     occurs.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more
        ///     required arguments are
        ///     null.
        /// </exception>
        /// <param name="negativeLookaheadAssertion">   The negative look-ahead assertion. </param>
        /// <returns>   NegativeLookaheadAssertion as a string. </returns>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.IRegexStringifier.ToNegativeLookaheadAssertionString(RegexNegativeLookaheadAssertionNode)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly",
            Justification = "Reviewed. Suppression is OK here.")]
        public virtual string ToNegativeLookaheadAssertionString(
            RegexNegativeLookaheadAssertionNode negativeLookaheadAssertion)
        {
            if (!IsNegativeLookaheadAssertionSupported)
                throw new NegativeLookaheadAssertionNotSupportedException(RegexLanguage);

            if (negativeLookaheadAssertion == null) throw new ArgumentNullException(nameof(negativeLookaheadAssertion));

            var sb = new StringBuilder();

            sb.Append(ToTokenString(RegexToken.NegativeLookaheadAssertionOpen))
                .Append(negativeLookaheadAssertion.IsInnerNodeIncluded
                    ? ToString(negativeLookaheadAssertion.InnerNode)
                    : negativeLookaheadAssertion.Pattern)
                .Append(ToTokenString(RegexToken.NegativeLookaheadAssertionClose));

            return AddQuantifier(
                sb.ToString(),
                negativeLookaheadAssertion.Minimum,
                negativeLookaheadAssertion.Maximum,
                negativeLookaheadAssertion.RegexQuantifierOption);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Converts a positiveLookbehindAssertion to a positive look-behind assertion string.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="PositiveLookbehindAssertionNotSupportedException">
        ///     Thrown when a Positive
        ///     Look-behind Assertion Not
        ///     Supported error condition
        ///     occurs.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more
        ///     required arguments are
        ///     null.
        /// </exception>
        /// <param name="positiveLookbehindAssertion">  The positive look-behind assertion. </param>
        /// <returns>   PositiveLookbehindAssertion as a string. </returns>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.IRegexStringifier.ToPositiveLookbehindAssertionString(RegexPositiveLookbehindAssertionNode)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly",
            Justification = "Reviewed. Suppression is OK here.")]
        public virtual string ToPositiveLookbehindAssertionString(
            RegexPositiveLookbehindAssertionNode positiveLookbehindAssertion)
        {
            if (!IsPositiveLookbehindAssertionSupported)
                throw new PositiveLookbehindAssertionNotSupportedException(RegexLanguage);

            if (positiveLookbehindAssertion == null)
                throw new ArgumentNullException(nameof(positiveLookbehindAssertion));

            var sb = new StringBuilder();

            sb.Append(ToTokenString(RegexToken.PositiveLookbehindAssertionOpen))
                .Append(positiveLookbehindAssertion.IsInnerNodeIncluded
                    ? ToString(positiveLookbehindAssertion.InnerNode)
                    : positiveLookbehindAssertion.Pattern)
                .Append(ToTokenString(RegexToken.PositiveLookbehindAssertionClose));

            return AddQuantifier(
                sb.ToString(),
                positiveLookbehindAssertion.Minimum,
                positiveLookbehindAssertion.Maximum,
                positiveLookbehindAssertion.RegexQuantifierOption);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Converts a negativeLookbehindAssertion to a negative look-behind assertion string.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="NegativeLookbehindAssertionNotSupportedException">
        ///     Thrown when a Negative
        ///     Look-behind Assertion Not
        ///     Supported error condition
        ///     occurs.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more
        ///     required arguments are
        ///     null.
        /// </exception>
        /// <param name="negativeLookbehindAssertion">  The negative look-behind assertion. </param>
        /// <returns>   NegativeLookbehindAssertion as a string. </returns>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.IRegexStringifier.ToNegativeLookbehindAssertionString(RegexNegativeLookbehindAssertionNode)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual string ToNegativeLookbehindAssertionString(
            RegexNegativeLookbehindAssertionNode negativeLookbehindAssertion)
        {
            if (!IsNegativeLookbehindAssertionSupported)
                throw new NegativeLookbehindAssertionNotSupportedException(RegexLanguage);

            if (negativeLookbehindAssertion == null)
                throw new ArgumentNullException(nameof(negativeLookbehindAssertion));

            var sb = new StringBuilder();

            sb.Append(ToTokenString(RegexToken.NegativeLookbehindAssertionOpen))
                .Append(negativeLookbehindAssertion.IsInnerNodeIncluded
                    ? ToString(negativeLookbehindAssertion.InnerNode)
                    : negativeLookbehindAssertion.Pattern)
                .Append(ToTokenString(RegexToken.NegativeLookbehindAssertionClose));

            return AddQuantifier(
                sb.ToString(),
                negativeLookbehindAssertion.Minimum,
                negativeLookbehindAssertion.Maximum,
                negativeLookbehindAssertion.RegexQuantifierOption);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Converts a conditionalNode to a conditional string. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="ConditionalNotSupportedException">
        ///     Thrown when a Conditional Not Supported
        ///     error condition occurs.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required
        ///     arguments are null.
        /// </exception>
        /// <param name="conditionalNode">  The conditional node. </param>
        /// <returns>   ConditionalNode as a string. </returns>
        /// <seealso cref="M:StatementIQ.Common.Library.RegEx.IRegexStringifier.ToConditionalString(RegexConditionalNode)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual string ToConditionalString(RegexConditionalNode conditionalNode)
        {
            if (!IsConditionalSupported) throw new ConditionalNotSupportedException(RegexLanguage);

            if (conditionalNode == null) throw new ArgumentNullException(nameof(conditionalNode));

            var sb = new StringBuilder();

            sb.Append(ToTokenString(RegexToken.ConditionalOpen));
            if (string.IsNullOrWhiteSpace(conditionalNode.NameOrNumber))
                sb.Append(ToTokenString(RegexToken.PositiveLookaheadAssertionOpen))
                    .Append(conditionalNode.IsInnerNodeIncluded
                        ? conditionalNode.InnerNode.ToString()
                        : conditionalNode.Pattern)
                    .Append(ToTokenString(RegexToken.PositiveLookaheadAssertionClose));
            else
                sb.Append(ToTokenString(RegexToken.GroupOpen))
                    .Append(conditionalNode.NameOrNumber)
                    .Append(ToTokenString(RegexToken.GroupClose));

            sb.Append(conditionalNode.TrueValue)
                .Append(ToTokenString(RegexToken.Alternation))
                .Append(conditionalNode.FalseValue)
                .Append(ToTokenString(RegexToken.ConditionalClose));

            return AddQuantifier(
                sb.ToString(),
                conditionalNode.Minimum,
                conditionalNode.Maximum,
                conditionalNode.RegexQuantifierOption);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Converts an unicodeCategoryNode to an unicode category string. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="UnicodeCategoryNotSupportedException">
        ///     Thrown when an Unicode Category Not
        ///     Supported error condition occurs.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required
        ///     arguments are null.
        /// </exception>
        /// <param name="unicodeCategoryNode">  The unicode category node. </param>
        /// <returns>   UnicodeCategoryNode as a string. </returns>
        /// <seealso cref="M:StatementIQ.Common.Library.RegEx.IRegexStringifier.ToUnicodeCategoryString(RegexUnicodeCategoryNode)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public virtual string ToUnicodeCategoryString(RegexUnicodeCategoryNode unicodeCategoryNode)
        {
            if (!IsUnicodeCategorySupported) throw new UnicodeCategoryNotSupportedException(RegexLanguage);

            if (unicodeCategoryNode == null) throw new ArgumentNullException(nameof(unicodeCategoryNode));

            var sb = new StringBuilder();

            if (!unicodeCategoryNode.Negative)
                sb.Append(ToTokenString(RegexToken.UnicodeCategoryOpen))
                    .Append(unicodeCategoryNode.Pattern)
                    .Append(ToTokenString(RegexToken.UnicodeCategoryClose));
            else
                sb.Append(ToTokenString(RegexToken.NegativeUnicodeCategoryOpen))
                    .Append(unicodeCategoryNode.Pattern)
                    .Append(ToTokenString(RegexToken.NegativeUnicodeCategoryClose));

            return AddQuantifier(
                sb.ToString(),
                unicodeCategoryNode.Minimum,
                unicodeCategoryNode.Maximum,
                unicodeCategoryNode.RegexQuantifierOption);
        }
    }
}
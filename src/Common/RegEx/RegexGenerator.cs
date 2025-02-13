using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using StatementIQ.RegEx.Exceptions;

namespace StatementIQ.RegEx
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   The main RegEx generator class. </summary>
    /// <remarks>   StatementIQ, 5/14/2020. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RegexGenerator
    {
        /// <summary>   The list. </summary>
        public readonly IList<RegexNode> List;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the StatementIQ.Common.Library.RegEx.RegexGenerator class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator()
            : this(RegexLanguage.DotNet)
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the StatementIQ.Common.Library.RegEx.RegexGenerator class.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="regexLanguage">    The RegEx language. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator(RegexLanguage regexLanguage)
        {
            SetRegexLanguage(regexLanguage);
            List = new List<RegexNode>();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the RegEx language. </summary>
        /// <value> The RegEx language. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public RegexLanguage RegexLanguage { get; private set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the RegEx language strategy. </summary>
        /// <value> The RegEx language strategy. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public RegexLanguageStrategy RegexLanguageStrategy { get; private set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets a value indicating whether the Language Strategy provides that the named capturing group is supported.
        /// </summary>
        /// <value> True if the named capturing group is supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool IsNamedCapturingGroupSupported => RegexLanguageStrategy.Stringifier.IsNamedCapturingGroupSupported;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets a value indicating whether the Language Strategy provides that the group options is supported.
        /// </summary>
        /// <value> True if the inline group options is supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool IsInlineGroupOptionsSupported => RegexLanguageStrategy.Stringifier.IsInlineGroupOptionsSupported;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a value indicating whether the Language Strategy provides that the atomic group is supported. </summary>
        /// <value> True if the atomic group is supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool IsAtomicGroupSupported => RegexLanguageStrategy.Stringifier.IsAtomicGroupSupported;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets a value indicating whether the Language Strategy provides that the look-ahead assertion is supported.
        /// </summary>
        /// <value> True if the positive look-ahead assertion is supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool IsPositiveLookaheadAssertionSupported =>
            RegexLanguageStrategy.Stringifier.IsPositiveLookaheadAssertionSupported;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets a value indicating whether the Language Strategy provides that the negative look-ahead assertion is supported.
        /// </summary>
        /// <value> True if the negative look-ahead assertion is supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool IsNegativeLookaheadAssertionSupported =>
            RegexLanguageStrategy.Stringifier.IsNegativeLookaheadAssertionSupported;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets a value indicating whether the Language Strategy provides that the positive look-behind assertion is
        ///     supported.
        /// </summary>
        /// <value> True if the positive look-behind assertion is supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool IsPositiveLookbehindAssertionSupported =>
            RegexLanguageStrategy.Stringifier.IsPositiveLookbehindAssertionSupported;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets a value indicating whether the Language Strategy provides that the negative look-behind assertion is
        ///     supported.
        /// </summary>
        /// <value> True if the negative look-behind assertion is supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool IsNegativeLookbehindAssertionSupported =>
            RegexLanguageStrategy.Stringifier.IsNegativeLookbehindAssertionSupported;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a value indicating whether the Language Strategy provides that the unicode category is supported. </summary>
        /// <value> True if the unicode category is supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool IsUnicodeCategorySupported => RegexLanguageStrategy.Stringifier.IsUnicodeCategorySupported;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a value indicating whether the Language Strategy provides that the conditional option is supported. </summary>
        /// <value> True if the conditional option is supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool IsConditionalSupported => RegexLanguageStrategy.Stringifier.IsConditionalSupported;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Sets the RegEx language. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="regexLanguage">    The RegEx strategy is then derived from the language. </param>
        /// <returns>   A RegexGenerator. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator SetRegexLanguage(RegexLanguage regexLanguage)
        {
            RegexLanguage = regexLanguage;
            RegexLanguageStrategy = new RegexLanguageStrategy(RegexLanguage);

            return this;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Clear the <see cref="List" />. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.Clear()" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator Clear()
        {
            List.Clear();

            return this;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Convert all nodes to string base on <see cref="RegexLanguage" />. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <returns>   Regex string for <see cref="RegexLanguage" /> </returns>
        /// <seealso cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.ToString()" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var node in List) sb.Append(node.ToString(RegexLanguageStrategy.Stringifier));
            return sb.ToString();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Create a <see cref="Regex" /> instance, with optional parameters. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="RegexLanguageNotSupportedException">
        ///     Thrown when a RegEx Language Not
        ///     Supported error condition occurs.
        /// </exception>
        /// <param name="options">
        ///     (Optional) Optional <see cref="RegexOptions" /> parameter for
        ///     creating <see cref="Regex" />.
        /// </param>
        /// <param name="matchTimeout">
        ///     (Optional) Optional <see cref="TimeSpan" /> parameter for creating
        ///     <see cref="Regex" />.
        /// </param>
        /// <returns>   New <see cref="Regex" /> object. </returns>
        /// <seealso cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.Create(RegexOptions?,TimeSpan?)" />
        /// <seealso cref="Regex" />
        /// <exception cref="NotSupportedException">
        ///     <see cref="RegexLanguage" /> is not
        ///     <see cref="RegexLanguage.DotNet" />
        /// </exception>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public Regex Create(RegexOptions? options = null, TimeSpan? matchTimeout = null)
        {
            if (RegexLanguage != RegexLanguage.DotNet)
                throw new RegexLanguageNotSupportedException(RegexLanguage,
                    $"Only {RegexLanguage.DotNet} {nameof(Regex)} object can be created.");

            if (options != null)
                return matchTimeout != null
                    ? new Regex(ToString(), options.Value, matchTimeout.Value)
                    : new Regex(ToString(), options.Value);

            return new Regex(ToString());
        }

        #region Add

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Create and add <see cref="RegexNode" /> to the generator. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="pattern">          Regex pattern. </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.Add(string,int?,int?,RegexQuantifierOption)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator Add(string pattern, int? min = null, int? max = null,
            RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var node = new RegexNode(pattern, min, max, quantifierOption);
            return Add(node);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Create and add <see cref="RegexNode" /> to the generator, and include another
        ///     <see cref="RegexNode" /> inside.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="innerNode">        An existing <see cref="RegexNode" /> to be included. </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.Add(RegexNode,int?,int?,RegexQuantifierOption)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator Add(RegexNode innerNode, int? min = null, int? max = null,
            RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var node = new RegexNode(innerNode, min, max, quantifierOption);
            return Add(node);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Add an existing <see cref="RegexNode" /> to the generator. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="ArgumentNullException">    <paramref name="node" /> is null. </exception>
        /// <param name="node"> <see cref="RegexNode" /> to be added. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.Add(RegexNode)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator Add(RegexNode node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            List.Add(node);

            return this;
        }

        #endregion

        #region AddGroup

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Create and add <see cref="RegexGroupNode" /> to the generator. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="pattern">          Regex pattern. </param>
        /// <param name="capturing">        (Optional) Defines the group to be capturing or not. </param>
        /// <param name="name">             (Optional) Optional name of capture group. </param>
        /// <param name="options">          (Optional) Optional inline options for the group. </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.AddGroup(string,bool,string,string,int?,int?,RegexQuantifierOption)" />
        /// ###
        /// <exception cref="NamedCapturingGroupNotSupportedException">
        ///     Named capturing group is not
        ///     supported by
        ///     <see cref="RegexLanguage" />.
        /// </exception>
        /// ###
        /// <exception cref="InlineGroupOptionsNotSupportedException">
        ///     Inline group option is not
        ///     supported by
        ///     <see cref="RegexLanguage" />.
        /// </exception>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator AddGroup(string pattern, bool capturing = true, string name = null, string options = null,
            int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var groupNode = new RegexGroupNode(pattern, capturing, name, options, min, max, quantifierOption);
            return AddGroup(groupNode);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Create and add <see cref="RegexGroupNode" /> to the generator, and include another
        ///     <see cref="RegexNode" /> inside.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="node">             An existing <see cref="RegexNode" /> to be included. </param>
        /// <param name="capturing">        (Optional) Defines the group to be capturing or not. </param>
        /// <param name="name">             (Optional) Optional name of capture group. </param>
        /// <param name="options">          (Optional) Optional inline options for the group. </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.AddGroup(RegexNode,bool,string,string,int?,int?,RegexQuantifierOption)" />
        /// ###
        /// <exception cref="NamedCapturingGroupNotSupportedException">
        ///     Named capturing group is not
        ///     supported by
        ///     <see cref="RegexLanguage" />.
        /// </exception>
        /// ###
        /// <exception cref="InlineGroupOptionsNotSupportedException">
        ///     Inline group option is not
        ///     supported by
        ///     <see cref="RegexLanguage" />.
        /// </exception>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator AddGroup(RegexNode node, bool capturing = true, string name = null, string options = null,
            int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var groupNode = new RegexGroupNode(node.ToString(RegexLanguageStrategy.Stringifier), capturing, name,
                options, min, max, quantifierOption);
            return AddGroup(groupNode);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Add an existing <see cref="RegexGroupNode" /> to the generator. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="NamedCapturingGroupNotSupportedException">
        ///     Named capturing group is not
        ///     supported by
        ///     <see cref="RegexLanguage" />.
        /// </exception>
        /// <exception cref="InlineGroupOptionsNotSupportedException">
        ///     Inline group option is not
        ///     supported by
        ///     <see cref="RegexLanguage" />.
        /// </exception>
        /// <param name="group">    <see cref="RegexGroupNode" /> to be added. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.AddGroup(RegexGroupNode)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator AddGroup(RegexGroupNode group)
        {
            if (!IsNamedCapturingGroupSupported && !string.IsNullOrWhiteSpace(group.Name))
                throw new NamedCapturingGroupNotSupportedException(RegexLanguage);

            if (!IsInlineGroupOptionsSupported && !string.IsNullOrWhiteSpace(group.Options))
                throw new InlineGroupOptionsNotSupportedException(RegexLanguage);

            return Add(group);
        }

        #endregion

        #region AddAtomicGroup

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Create and add <see cref="RegexAtomicGroupNode" /> to the generator. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="pattern">          Regex pattern. </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.AddAtomicGroup(string,int?,int?,RegexQuantifierOption)" />
        /// ###
        /// <exception cref="AtomicGroupNotSupportedException">
        ///     Atomic group is not supported by
        ///     <see cref="RegexLanguage" />.
        /// </exception>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator AddAtomicGroup(string pattern, int? min = null, int? max = null,
            RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var atomicGroup = new RegexAtomicGroupNode(pattern, min, max, quantifierOption);
            return AddAtomicGroup(atomicGroup);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Create and add <see cref="RegexAtomicGroupNode" /> to the generator, and include another
        ///     <see cref="RegexNode" /> inside.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="innerNode">        An existing <see cref="RegexNode" /> to be included. </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.AddAtomicGroup(RegexNode,int?,int?,RegexQuantifierOption)" />
        /// ###
        /// <exception cref="AtomicGroupNotSupportedException">
        ///     Atomic group is not supported by
        ///     <see cref="RegexLanguage" />.
        /// </exception>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator AddAtomicGroup(RegexNode innerNode, int? min = null, int? max = null,
            RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var atomicGroup = new RegexAtomicGroupNode(innerNode, min, max, quantifierOption);
            return AddAtomicGroup(atomicGroup);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Add an existing <see cref="RegexAtomicGroupNode" /> to the generator. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="AtomicGroupNotSupportedException">
        ///     Atomic group is not supported by
        ///     <see cref="RegexLanguage" />.
        /// </exception>
        /// <param name="atomicGroup">  Group the atomic belongs to. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.AddAtomicGroup(RegexAtomicGroupNode)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator AddAtomicGroup(RegexAtomicGroupNode atomicGroup)
        {
            if (!IsAtomicGroupSupported) throw new AtomicGroupNotSupportedException(RegexLanguage);

            return Add(atomicGroup);
        }

        #endregion

        #region AddPositiveLookaheadAssertion

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Create and add <see cref="RegexPositiveLookaheadAssertionNode" /> to the generator.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="pattern">          Regex pattern. </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.AddPositiveLookaheadAssertion(string,int?,int?,RegexQuantifierOption)" />
        /// ###
        /// <exception cref="PositiveLookaheadAssertionNotSupportedException">
        ///     Positive look-ahead
        ///     assertion is not
        ///     supported by
        ///     <see cref="RegexLanguage" />.
        /// </exception>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator AddPositiveLookaheadAssertion(string pattern, int? min = null, int? max = null,
            RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var positiveLookaheadAssertion =
                new RegexPositiveLookaheadAssertionNode(pattern, min, max, quantifierOption);
            return AddPositiveLookaheadAssertion(positiveLookaheadAssertion);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Create and add <see cref="RegexPositiveLookaheadAssertionNode" /> to the generator, and
        ///     include another <see cref="RegexNode" /> inside.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="innerNode">        An existing <see cref="RegexNode" /> to be included. </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.AddPositiveLookaheadAssertion(RegexNode,int?,int?,RegexQuantifierOption)" />
        /// ###
        /// <exception cref="PositiveLookaheadAssertionNotSupportedException">
        ///     Positive look-ahead
        ///     assertion is not
        ///     supported by
        ///     <see cref="RegexLanguage" />.
        /// </exception>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator AddPositiveLookaheadAssertion(RegexNode innerNode, int? min = null, int? max = null,
            RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var positiveLookaheadAssertion =
                new RegexPositiveLookaheadAssertionNode(innerNode, min, max, quantifierOption);
            return AddPositiveLookaheadAssertion(positiveLookaheadAssertion);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Add an existing <see cref="RegexPositiveLookaheadAssertionNode" /> to the generator.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="PositiveLookaheadAssertionNotSupportedException">
        ///     Positive look-ahead
        ///     assertion is not
        ///     supported by
        ///     <see cref="RegexLanguage" />.
        /// </exception>
        /// <param name="positiveLookaheadAssertion">   The positive look-ahead assertion. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.AddPositiveLookaheadAssertion(RegexPositiveLookaheadAssertionNode)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator AddPositiveLookaheadAssertion(
            RegexPositiveLookaheadAssertionNode positiveLookaheadAssertion)
        {
            if (!IsPositiveLookaheadAssertionSupported)
                throw new PositiveLookaheadAssertionNotSupportedException(RegexLanguage);

            return Add(positiveLookaheadAssertion);
        }

        #endregion

        #region AddNegativeLookaheadAssertion

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Create and add <see cref="RegexNegativeLookaheadAssertionNode" /> to the generator.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="pattern">          Regex pattern. </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.AddNegativeLookaheadAssertion(string,int?,int?,RegexQuantifierOption)" />
        /// ###
        /// <exception cref="NegativeLookaheadAssertionNotSupportedException">
        ///     Negative look-ahead
        ///     assertion is not
        ///     supported by
        ///     <see cref="RegexLanguage" />.
        /// </exception>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator AddNegativeLookaheadAssertion(string pattern, int? min = null, int? max = null,
            RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var negativeLookaheadAssertion =
                new RegexNegativeLookaheadAssertionNode(pattern, min, max, quantifierOption);
            return AddNegativeLookaheadAssertion(negativeLookaheadAssertion);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Create and add <see cref="RegexNegativeLookaheadAssertionNode" /> to the generator, and
        ///     include another <see cref="RegexNode" /> inside.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="innerNode">        An existing <see cref="RegexNode" /> to be included. </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.AddNegativeLookaheadAssertion(RegexNode,int?,int?,RegexQuantifierOption)" />
        /// ###
        /// <exception cref="NegativeLookaheadAssertionNotSupportedException">
        ///     Negative look-ahead
        ///     assertion is not
        ///     supported by
        ///     <see cref="RegexLanguage" />.
        /// </exception>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator AddNegativeLookaheadAssertion(RegexNode innerNode, int? min = null, int? max = null,
            RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var negativeLookaheadAssertion =
                new RegexNegativeLookaheadAssertionNode(innerNode, min, max, quantifierOption);
            return AddNegativeLookaheadAssertion(negativeLookaheadAssertion);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Add an existing <see cref="RegexNegativeLookaheadAssertionNode" /> to the generator.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="NegativeLookaheadAssertionNotSupportedException">
        ///     Negative look-ahead
        ///     assertion is not
        ///     supported by
        ///     <see cref="RegexLanguage" />.
        /// </exception>
        /// <param name="negativeLookaheadAssertion">   The negative look-ahead assertion. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.AddNegativeLookaheadAssertion(RegexNegativeLookaheadAssertionNode)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator AddNegativeLookaheadAssertion(
            RegexNegativeLookaheadAssertionNode negativeLookaheadAssertion)
        {
            if (!IsNegativeLookaheadAssertionSupported)
                throw new NegativeLookaheadAssertionNotSupportedException(RegexLanguage);

            return Add(negativeLookaheadAssertion);
        }

        #endregion

        #region AddPositiveLookbehindAssertion

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Create and add <see cref="RegexPositiveLookbehindAssertionNode" /> to the generator.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="pattern">          Regex pattern. </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.AddPositiveLookbehindAssertion(string,int?,int?,RegexQuantifierOption)" />
        /// ###
        /// <exception cref="PositiveLookbehindAssertionNotSupportedException">
        ///     Positive look-behind
        ///     assertion is not
        ///     supported by
        ///     <see cref="RegexLanguage" />.
        /// </exception>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator AddPositiveLookbehindAssertion(string pattern, int? min = null, int? max = null,
            RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var positiveLookbehnidAssertion =
                new RegexPositiveLookbehindAssertionNode(pattern, min, max, quantifierOption);
            return AddPositiveLookbehindAssertion(positiveLookbehnidAssertion);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Create and add <see cref="RegexPositiveLookbehindAssertionNode" /> to the generator, and
        ///     include another <see cref="RegexNode" /> inside.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="innerNode">        An existing <see cref="RegexNode" /> to be included. </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.AddPositiveLookbehindAssertion(RegexNode,int?,int?,RegexQuantifierOption)" />
        /// ###
        /// <exception cref="PositiveLookbehindAssertionNotSupportedException">
        ///     Positive look-behind
        ///     assertion is not
        ///     supported by
        ///     <see cref="RegexLanguage" />.
        /// </exception>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator AddPositiveLookbehindAssertion(RegexNode innerNode, int? min = null, int? max = null,
            RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var positiveLookbehnidAssertion =
                new RegexPositiveLookbehindAssertionNode(innerNode, min, max, quantifierOption);
            return AddPositiveLookbehindAssertion(positiveLookbehnidAssertion);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Add an existing <see cref="RegexPositiveLookbehindAssertionNode" /> to the generator.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="PositiveLookbehindAssertionNotSupportedException">
        ///     Positive look-behind
        ///     assertion is not
        ///     supported by
        ///     <see cref="RegexLanguage" />.
        /// </exception>
        /// <param name="positiveLookbehnidAssertion">  The positive look-behind assertion. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.AddPositiveLookbehindAssertion(RegexPositiveLookbehindAssertionNode)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator AddPositiveLookbehindAssertion(
            RegexPositiveLookbehindAssertionNode positiveLookbehnidAssertion)
        {
            if (!IsPositiveLookbehindAssertionSupported)
                throw new PositiveLookbehindAssertionNotSupportedException(RegexLanguage);

            return Add(positiveLookbehnidAssertion);
        }

        #endregion

        #region AddNegativeLookbehindAssertion

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Create and add <see cref="RegexNegativeLookbehindAssertionNode" /> to the generator.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="pattern">          Regex pattern. </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.AddNegativeLookbehindAssertion(string,int?,int?,RegexQuantifierOption)" />
        /// ###
        /// <exception cref="NegativeLookbehindAssertionNotSupportedException">
        ///     Negative look-behind
        ///     assertion is not
        ///     supported by
        ///     <see cref="RegexLanguage" />.
        /// </exception>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator AddNegativeLookbehindAssertion(string pattern, int? min = null, int? max = null,
            RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var negativeLookbehindAssertion =
                new RegexNegativeLookbehindAssertionNode(pattern, min, max, quantifierOption);
            return AddNegativeLookbehindAssertion(negativeLookbehindAssertion);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Create and add <see cref="RegexNegativeLookbehindAssertionNode" /> to the generator, and
        ///     include another <see cref="RegexNode" /> inside.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="innerNode">        An existing <see cref="RegexNode" /> to be included. </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.AddNegativeLookbehindAssertion(RegexNode,int?,int?,RegexQuantifierOption)" />
        /// ###
        /// <exception cref="NegativeLookbehindAssertionNotSupportedException">
        ///     Negative look-behind
        ///     assertion is not
        ///     supported by
        ///     <see cref="RegexLanguage" />.
        /// </exception>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator AddNegativeLookbehindAssertion(RegexNode innerNode, int? min = null, int? max = null,
            RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var negativeLookbehindAssertion =
                new RegexNegativeLookbehindAssertionNode(innerNode, min, max, quantifierOption);
            return AddNegativeLookbehindAssertion(negativeLookbehindAssertion);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Add an existing <see cref="RegexNegativeLookbehindAssertionNode" /> to the generator.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="NegativeLookbehindAssertionNotSupportedException">
        ///     Negative look-behind
        ///     assertion is not
        ///     supported by
        ///     <see cref="RegexLanguage" />.
        /// </exception>
        /// <param name="negativeLookbehindAssertion">  The negative look-behind assertion. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.AddNegativeLookbehindAssertion(RegexNegativeLookbehindAssertionNode)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator AddNegativeLookbehindAssertion(
            RegexNegativeLookbehindAssertionNode negativeLookbehindAssertion)
        {
            if (!IsNegativeLookbehindAssertionSupported)
                throw new NegativeLookbehindAssertionNotSupportedException(RegexLanguage);

            return Add(negativeLookbehindAssertion);
        }

        #endregion

        #region AddConditional

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Create and add <see cref="RegexConditionalNode" /> to the generator. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="pattern">          Regex pattern. </param>
        /// <param name="trueValue">        Value of successful match. </param>
        /// <param name="falseValue">       Value of failed match. </param>
        /// <param name="nameOrNumber">     (Optional) Name or number for backtracking. </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.AddConditional(string,string,string,string,int?,int?,RegexQuantifierOption)" />
        /// ###
        /// <exception cref="ConditionalNotSupportedException">
        ///     Conditional is not supported by
        ///     <see cref="RegexLanguage" />.
        /// </exception>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator AddConditional(string pattern, string trueValue, string falseValue,
            string nameOrNumber = null, int? min = null, int? max = null,
            RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var conditionalNode = new RegexConditionalNode(pattern, trueValue, falseValue, nameOrNumber, min, max,
                quantifierOption);
            return AddConditional(conditionalNode);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Create and add <see cref="RegexConditionalNode" /> to the generator, and include another
        ///     <see cref="RegexNode" /> inside.
        /// </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="innerNode">        An existing <see cref="RegexNode" /> to be included. </param>
        /// <param name="trueValue">        Value of successful match. </param>
        /// <param name="falseValue">       Value of failed match. </param>
        /// <param name="nameOrNumber">     (Optional) Name or number for backtracking. </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.AddConditional(RegexNode,string,string,string,int?,int?,RegexQuantifierOption)" />
        /// ###
        /// <exception cref="ConditionalNotSupportedException">
        ///     Conditional is not supported by
        ///     <see cref="RegexLanguage" />.
        /// </exception>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator AddConditional(RegexNode innerNode, string trueValue, string falseValue,
            string nameOrNumber = null, int? min = null, int? max = null,
            RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var conditionalNode = new RegexConditionalNode(innerNode, trueValue, falseValue, nameOrNumber, min, max,
                quantifierOption);
            return AddConditional(conditionalNode);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Add an existing <see cref="RegexConditionalNode" /> to the generator. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="ConditionalNotSupportedException">
        ///     Conditional is not supported by
        ///     <see cref="RegexLanguage" />.
        /// </exception>
        /// <param name="conditional">  <see cref="RegexConditionalNode" /> to be added. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.AddConditional(RegexConditionalNode)" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator AddConditional(RegexConditionalNode conditional)
        {
            if (!IsConditionalSupported) throw new ConditionalNotSupportedException(RegexLanguage);

            return Add(conditional);
        }

        #endregion

        #region AddUnicodeCategory

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Create and add <see cref="RegexUnicodeCategoryNode" /> to the generator. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="unicodeCategory">  Value from <see cref="RegexUnicodeCategoryFlag" />. </param>
        /// <param name="negative">
        ///     (Optional) Optional value to determine if negate the
        ///     category.
        /// </param>
        /// <param name="min">              (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">              (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption"> (Optional) Optional quantifier option. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.AddUnicodeCategory(RegexUnicodeCategoryFlag,bool,int?,int?,RegexQuantifierOption)" />
        /// <seealso cref="RegexUnicodeCategoryNode(RegexUnicodeCategoryFlag, bool, int?, int?, RegexQuantifierOption)" />
        /// <seealso cref="AddUnicodeCategory(RegexUnicodeCategoryNode)" />
        /// ###
        /// <exception cref="UnicodeCategoryNotSupportedException">
        ///     Unicode category is not supported
        ///     by <see cref="RegexLanguage" />.
        /// </exception>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator AddUnicodeCategory(RegexUnicodeCategoryFlag unicodeCategory, bool negative = false,
            int? min = null, int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var unicodeCategoryNode =
                new RegexUnicodeCategoryNode(unicodeCategory, negative, min, max, quantifierOption);
            return AddUnicodeCategory(unicodeCategoryNode);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Create and add <see cref="RegexUnicodeCategoryNode" /> to the generator. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <param name="unicodeDesignation">   Unicode designation or named block. </param>
        /// <param name="negative">
        ///     (Optional) Optional value to determine if negate the
        ///     category.
        /// </param>
        /// <param name="min">                  (Optional) Optional minimum number of occurrences. </param>
        /// <param name="max">                  (Optional) Optional maximum number of occurrences. </param>
        /// <param name="quantifierOption">     (Optional) Optional quantifier option. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso
        ///     cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.AddUnicodeCategory(string,bool,int?,int?,RegexQuantifierOption)" />
        /// <seealso cref="RegexUnicodeCategoryNode(string, bool, int?, int?, RegexQuantifierOption)" />
        /// <seealso cref="AddUnicodeCategory(RegexUnicodeCategoryNode)" />
        /// ###
        /// <exception cref="UnicodeCategoryNotSupportedException">
        ///     Unicode category is not supported
        ///     by <see cref="RegexLanguage" />.
        /// </exception>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator AddUnicodeCategory(string unicodeDesignation, bool negative = false, int? min = null,
            int? max = null, RegexQuantifierOption quantifierOption = RegexQuantifierOption.Greedy)
        {
            var unicodeCategoryNode =
                new RegexUnicodeCategoryNode(unicodeDesignation, negative, min, max, quantifierOption);
            return AddUnicodeCategory(unicodeCategoryNode);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Add an existing <see cref="RegexUnicodeCategoryNode" /> to the generator. </summary>
        /// <remarks>   StatementIQ, 5/14/2020. </remarks>
        /// <exception cref="UnicodeCategoryNotSupportedException">
        ///     Unicode category is not supported by
        ///     <see cref="RegexLanguage" />.
        /// </exception>
        /// <param name="unicodeCategory">  <see cref="RegexUnicodeCategoryNode" /> to be added. </param>
        /// <returns>
        ///     <see cref="RegexGenerator" />
        /// </returns>
        /// <seealso cref="M:StatementIQ.Common.Library.RegEx.RegexGenerator.AddUnicodeCategory(RegexUnicodeCategoryNode)" />
        /// <seealso cref="RegexUnicodeCategoryNode" />
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public RegexGenerator AddUnicodeCategory(RegexUnicodeCategoryNode unicodeCategory)
        {
            if (!IsUnicodeCategorySupported) throw new UnicodeCategoryNotSupportedException(RegexLanguage);

            return Add(unicodeCategory);
        }

        #endregion
    }
}
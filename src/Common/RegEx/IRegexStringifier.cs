namespace StatementIQ.RegEx
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Interface for the RegEx stringifier. </summary>
    /// <remarks>   StatementIQ, 5/14/2020. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public interface IRegexStringifier
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the RegEx language. </summary>
        /// <value> The RegEx language. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        RegexLanguage RegexLanguage { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets a value indicating whether the named capturing group option is supported.
        /// </summary>
        /// <value> True if this  is named capturing group supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        bool IsNamedCapturingGroupSupported { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets a value indicating whether the inline group options are supported.
        /// </summary>
        /// <value> True if this  is inline group options supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        bool IsInlineGroupOptionsSupported { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a value indicating whether the atomic group option is supported. </summary>
        /// <value> True if this  is atomic group supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        bool IsAtomicGroupSupported { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets a value indicating whether the positive look-ahead assertion option is supported.
        /// </summary>
        /// <value> True if this  is positive look-ahead assertion supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        bool IsPositiveLookaheadAssertionSupported { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets a value indicating whether the negative look-ahead assertion option is supported.
        /// </summary>
        /// <value> True if this  is negative look-ahead assertion supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        bool IsNegativeLookaheadAssertionSupported { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets a value indicating whether the positive look-behind assertion option is supported.
        /// </summary>
        /// <value> True if this  is positive look-behind assertion supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        bool IsPositiveLookbehindAssertionSupported { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets a value indicating whether the negative look-behind assertion option is supported.
        /// </summary>
        /// <value> True if this  is negative look-behind assertion supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        bool IsNegativeLookbehindAssertionSupported { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a value indicating whether the unicode category option is supported. </summary>
        /// <value> True if this  is unicode category supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        bool IsUnicodeCategorySupported { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a value indicating whether the conditional option is supported. </summary>
        /// <value> True if this  is conditional supported, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        bool IsConditionalSupported { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Convert this into a string representation. </summary>
        /// <param name="node"> The node. </param>
        /// <returns>   A string that represents this. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        string ToString(RegexNode node);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Adds a quantifier. </summary>
        /// <param name="pattern">          Specifies the pattern. </param>
        /// <param name="min">              The minimum allowed number of characters. </param>
        /// <param name="max">              The maximum allowed number of characters. </param>
        /// <param name="quantifierOption"> The quantifier option. </param>
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        string AddQuantifier(string pattern, int? min, int? max, RegexQuantifierOption quantifierOption);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Converts a quantifierOption to a quantifier option string. </summary>
        /// <param name="quantifierOption"> The quantifier option. </param>
        /// <returns>   QuantifierOption as a string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        string ToQuantifierOptionString(RegexQuantifierOption quantifierOption);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Converts a token to a token string. </summary>
        /// <param name="token">    The token. </param>
        /// <returns>   Token as a string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        string ToTokenString(RegexToken token);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Converts a node to a node string. </summary>
        /// <param name="node"> The node. </param>
        /// <returns>   Node as a string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        string ToNodeString(RegexNode node);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Converts a group to a group string. </summary>
        /// <param name="group">    The group. </param>
        /// <returns>   Group as a string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        string ToGroupString(RegexGroupNode group);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Converts an atomicGroup to an atomic group string. </summary>
        /// <param name="atomicGroup">  Group the atomic belongs to. </param>
        /// <returns>   AtomicGroup as a string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        string ToAtomicGroupString(RegexAtomicGroupNode atomicGroup);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Converts a positiveLookaheadAssertion to a positive lookahead assertion string.
        /// </summary>
        /// <param name="positiveLookaheadAssertion">   The positive lookahead assertion. </param>
        /// <returns>   PositiveLookaheadAssertion as a string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        string ToPositiveLookaheadAssertionString(RegexPositiveLookaheadAssertionNode positiveLookaheadAssertion);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Converts a negativeLookaheadAssertion to a negative lookahead assertion string.
        /// </summary>
        /// <param name="negativeLookaheadAssertion">   The negative lookahead assertion. </param>
        /// <returns>   NegativeLookaheadAssertion as a string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        string ToNegativeLookaheadAssertionString(RegexNegativeLookaheadAssertionNode negativeLookaheadAssertion);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Converts a positiveLookbehindAssertion to a positive lookbehind assertion string.
        /// </summary>
        /// <param name="positiveLookbehindAssertion">  The positive lookbehind assertion. </param>
        /// <returns>   PositiveLookbehindAssertion as a string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        string ToPositiveLookbehindAssertionString(RegexPositiveLookbehindAssertionNode positiveLookbehindAssertion);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Converts a negativeLookbehindAssertion to a negative lookbehind assertion string.
        /// </summary>
        /// <param name="negativeLookbehindAssertion">  The negative lookbehind assertion. </param>
        /// <returns>   NegativeLookbehindAssertion as a string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        string ToNegativeLookbehindAssertionString(RegexNegativeLookbehindAssertionNode negativeLookbehindAssertion);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Converts a conditionalNode to a conditional string. </summary>
        /// <param name="conditionalNode">  The conditional node. </param>
        /// <returns>   ConditionalNode as a string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        string ToConditionalString(RegexConditionalNode conditionalNode);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Converts an unicodeCategoryNode to an unicode category string. </summary>
        /// <param name="unicodeCategoryNode">  The unicode category node. </param>
        /// <returns>   UnicodeCategoryNode as a string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        string ToUnicodeCategoryString(RegexUnicodeCategoryNode unicodeCategoryNode);
    }
}
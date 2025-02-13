namespace StatementIQ.RegEx
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Values that represent the various RegEx tokens. </summary>
    /// <remarks>   StatementIQ, 5/14/2020. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public enum RegexToken
    {
        /// <summary>   An enum constant representing any option. </summary>
        Any,

        /// <summary>   An enum constant representing the group open option. </summary>
        GroupOpen,

        /// <summary>   An enum constant representing the group non capturing option. </summary>
        GroupNonCapturing,

        /// <summary>   An enum constant representing the group option start option. </summary>
        GroupOptionStart,

        /// <summary>   An enum constant representing the group name open option. </summary>
        GroupNameOpen,

        /// <summary>   An enum constant representing the group name close option. </summary>
        GroupNameClose,

        /// <summary>   An enum constant representing the group option end option. </summary>
        GroupOptionEnd,

        /// <summary>   An enum constant representing the group close option. </summary>
        GroupClose,

        /// <summary>   An enum constant representing the conditional open option. </summary>
        ConditionalOpen,

        /// <summary>   An enum constant representing the conditional close option. </summary>
        ConditionalClose,

        /// <summary>   An enum constant representing the atomic group open option. </summary>
        AtomicGroupOpen,

        /// <summary>   An enum constant representing the atomic group close option. </summary>
        AtomicGroupClose,

        /// <summary>
        ///     An enum constant representing the positive lookahead assertion open option.
        /// </summary>
        PositiveLookaheadAssertionOpen,

        /// <summary>
        ///     An enum constant representing the positive lookahead assertion close option.
        /// </summary>
        PositiveLookaheadAssertionClose,

        /// <summary>
        ///     An enum constant representing the negative lookahead assertion open option.
        /// </summary>
        NegativeLookaheadAssertionOpen,

        /// <summary>
        ///     An enum constant representing the negative lookahead assertion close option.
        /// </summary>
        NegativeLookaheadAssertionClose,

        /// <summary>
        ///     An enum constant representing the positive lookbehind assertion open option.
        /// </summary>
        PositiveLookbehindAssertionOpen,

        /// <summary>
        ///     An enum constant representing the positive lookbehind assertion close option.
        /// </summary>
        PositiveLookbehindAssertionClose,

        /// <summary>
        ///     An enum constant representing the negative lookbehind assertion open option.
        /// </summary>
        NegativeLookbehindAssertionOpen,

        /// <summary>
        ///     An enum constant representing the negative lookbehind assertion close option.
        /// </summary>
        NegativeLookbehindAssertionClose,

        /// <summary>   An enum constant representing the character group open option. </summary>
        CharacterGroupOpen,

        /// <summary>   An enum constant representing the character group negative option. </summary>
        CharacterGroupNegative,

        /// <summary>   An enum constant representing the character group close option. </summary>
        CharacterGroupClose,

        /// <summary>   An enum constant representing the alternation option. </summary>
        Alternation,

        /// <summary>   An enum constant representing the zero or one option. </summary>
        ZeroOrOne,

        /// <summary>   An enum constant representing the zero or more option. </summary>
        ZeroOrMore,

        /// <summary>   An enum constant representing the one or more option. </summary>
        OneOrMore,

        /// <summary>   An enum constant representing the lazy option. </summary>
        Lazy,

        /// <summary>   An enum constant representing the possessive option. </summary>
        Possessive,

        /// <summary>   An enum constant representing the quantifier open option. </summary>
        QuantifierOpen,

        /// <summary>   An enum constant representing the quantifier separator option. </summary>
        QuantifierSeparator,

        /// <summary>   An enum constant representing the quantifier close option. </summary>
        QuantifierClose,

        /// <summary>   An enum constant representing the string start anchor option. </summary>
        StringStartAnchor,

        /// <summary>   An enum constant representing the string end anchor option. </summary>
        StringEndAnchor,

        /// <summary>   An enum constant representing the unicode category open option. </summary>
        UnicodeCategoryOpen,

        /// <summary>   An enum constant representing the unicode category close option. </summary>
        UnicodeCategoryClose,

        /// <summary>   An enum constant representing the negative unicode category open option. </summary>
        NegativeUnicodeCategoryOpen,

        /// <summary>   An enum constant representing the negative unicode category close option. </summary>
        NegativeUnicodeCategoryClose
    }
}
using System;
using System.Globalization;
using MandateThat;

namespace StatementIQ.RegEx
{
    public class RegexQuantifier
    {
        private int? maxOccurrenceCount;
        private int? minOccurrenceCount;

        /// <summary>
        ///     Initializes a new instance of RegexQuantifier.
        /// </summary>
        public RegexQuantifier()
        {
            IsLazy = false;
        }

        /// <summary>
        ///     Initializes a new instance of RegexQuantifier.
        /// </summary>
        /// <param name="minOccurrenceCount">Minimum occurrence count.</param>
        /// <param name="maxOccurrenceCount">Maximum occurrence count.</param>
        public RegexQuantifier(int? minOccurrenceCount, int? maxOccurrenceCount)
            : this()
        {
            MinOccurrenceCount = minOccurrenceCount;
            MaxOccurrenceCount = maxOccurrenceCount;
        }

        /// <summary>
        ///     Initializes a new instance of RegexQuantifier.
        /// </summary>
        /// <param name="minOccurrenceCount">Minimum occurrence count.</param>
        /// <param name="maxOccurrenceCount">Maximum occurrence count.</param>
        /// <param name="isLazy">True - use lazy quantifier. False - use greedy quantifier.</param>
        public RegexQuantifier(int? minOccurrenceCount, int? maxOccurrenceCount, bool isLazy)
            : this(minOccurrenceCount, maxOccurrenceCount)
        {
            IsLazy = isLazy;
        }

        /// <summary>
        ///     Minimum occurrence count.
        /// </summary>
        public int? MinOccurrenceCount
        {
            get => minOccurrenceCount;
            set
            {
                Mandate.That(value.HasValue, nameof(value)).IsTrue();
                Mandate.That(value.Value, nameof(value)).IsGreaterThanOrEqualTo(0);
                minOccurrenceCount = value;
            }
        }

        /// <summary>
        ///     Maximum occurrence count. NULL = unlimited.
        /// </summary>
        public int? MaxOccurrenceCount
        {
            get => maxOccurrenceCount;
            set
            {
                if (value < 0) throw new ArgumentException("MaxOccurrenceCount cannot be negative.");

                //Mandate.That(value.Value, nameof(value)).IsGreaterThanOrEqualTo(0);
                maxOccurrenceCount = value;
            }
        }

        /// <summary>
        ///     Specifies whether the quantified expression should be matched as few times as possible.
        /// </summary>
        public bool IsLazy { get; set; }

        /// <summary>
        ///     NULL quantifier.
        /// </summary>
        public RegexQuantifier None => null;

        /// <summary>
        ///     The "*" quantifier.
        /// </summary>
        public RegexQuantifier ZeroOrMore => new RegexQuantifier(0, null, false);

        /// <summary>
        ///     The "*?" quantifier.
        /// </summary>
        public RegexQuantifier ZeroOrMoreLazy => new RegexQuantifier(0, null, true);

        /// <summary>
        ///     The "+" quantifier.
        /// </summary>
        public RegexQuantifier OneOrMore => new RegexQuantifier(1, null, false);

        /// <summary>
        ///     The "+?" quantifier.
        /// </summary>
        public RegexQuantifier OneOrMoreLazy => new RegexQuantifier(1, null, true);

        /// <summary>
        ///     The "?" quantifier.
        /// </summary>
        public RegexQuantifier ZeroOrOne => new RegexQuantifier(0, 1, false);

        /// <summary>
        ///     The "??" quantifier.
        /// </summary>
        public RegexQuantifier ZeroOrOneLazy => new RegexQuantifier(0, 1, true);

        /// <summary>
        ///     The "{n,}" quantifier.
        ///     <param name="minOccurrenceCount">Minimum occurrence count.</param>
        /// </summary>
        /// <returns>An instance of RegexQuantifier with the specified options.</returns>
        public RegexQuantifier AtLeast(int minOccurrenceCount)
        {
            return new RegexQuantifier(minOccurrenceCount, null, false);
        }

        /// <summary>
        ///     The "{n,}" or "{n,}?" quantifier.
        ///     <param name="minOccurrenceCount">Minimum occurrence count.</param>
        ///     <param name="isLazy">True - use lazy quantifier. False - use greedy quantifier.</param>
        /// </summary>
        /// <returns>An instance of RegexQuantifier with the specified options.</returns>
        public RegexQuantifier AtLeast(int minOccurrenceCount, bool isLazy)
        {
            return new RegexQuantifier(minOccurrenceCount, null, isLazy);
        }

        /// <summary>
        ///     The "{n}" or "{n}?" quantifier.
        ///     <param name="occurrenceCount">Exact occurrence count.</param>
        /// </summary>
        /// <returns>An instance of RegexQuantifier with the specified options.</returns>
        public RegexQuantifier Exactly(int occurrenceCount)
        {
            return new RegexQuantifier(occurrenceCount, occurrenceCount, false);
        }

        /// <summary>
        ///     The "{n}" or "{n}?" quantifier.
        ///     <param name="occurrenceCount">Exact occurrence count.</param>
        ///     <param name="isLazy">True - use lazy quantifier. False - use greedy quantifier.</param>
        /// </summary>
        /// <returns>An instance of RegexQuantifier with the specified options.</returns>
        public RegexQuantifier Exactly(int occurrenceCount, bool isLazy)
        {
            return new RegexQuantifier(occurrenceCount, occurrenceCount, isLazy);
        }

        /// <summary>
        ///     Custom "{n,m}" or "{n,m}?" quantifier.
        /// </summary>
        /// <param name="minOccurrenceCount">Minimum occurrence count.</param>
        /// <param name="maxOccurrenceCount">Maximum occurrence count.</param>
        /// <param name="isLazy">True - use lazy quantifier. False - use greedy quantifier.</param>
        /// <returns>An instance of RegexQuantifier with the specified options.</returns>
        public RegexQuantifier Custom(int? minOccurrenceCount, int? maxOccurrenceCount, bool isLazy)
        {
            return new RegexQuantifier(minOccurrenceCount, maxOccurrenceCount, isLazy);
        }

        /// <summary>
        ///     Converts RegexQuantifier to a Regex pattern string.
        /// </summary>
        /// <returns></returns>
        public virtual string ToRegexPattern()
        {
            string result;

            switch (MinOccurrenceCount)
            {
                case 0 when MaxOccurrenceCount == 1:
                    result = "?";
                    break;
                case 0 when MaxOccurrenceCount == null:
                    result = "*";
                    break;
                case 1 when MaxOccurrenceCount == null:
                    result = "+";
                    break;
                default:
                {
                    if (MinOccurrenceCount == MaxOccurrenceCount)
                        result = string.Format(CultureInfo.InvariantCulture, "{{{0}}}", MinOccurrenceCount);
                    else if (MaxOccurrenceCount == null)
                        result = string.Format(CultureInfo.InvariantCulture, "{{{0},}}", MinOccurrenceCount);
                    else
                        result = string.Format(CultureInfo.InvariantCulture, "{{{0},{1}}}", MinOccurrenceCount,
                            MaxOccurrenceCount);

                    break;
                }
            }

            if (IsLazy) result += "?";

            return result;
        }
    }
}
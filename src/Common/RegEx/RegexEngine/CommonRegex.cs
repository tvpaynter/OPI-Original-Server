using MandateThat;

//[assembly: InternalsVisibleTo("StatementIQ.Common.Test")]

namespace StatementIQ.RegEx.RegexEngine
{
    /// <summary>   A common regex. This class cannot be inherited. </summary>
    public class CommonRegex
    {
        #region Constructors

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Initializes a new instance of the StatementIQ.Common.RegexEngine.CommonRegex class.
        /// </summary>
        /// <param name="value">    The value. </param>
        /// <param name="name">     The name. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public CommonRegex(int value, string name)
        {
            Mandate.That(value >= 0, "value cannot be less than zero");
            Mandate.That(!string.IsNullOrWhiteSpace(name), "name cannot be null");

            Name = name;
            Value = value;
        }

        #endregion Constructors

        #region Private Members

        #endregion Private Members

        #region Public Properties

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the name. </summary>
        /// <value> The name. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Name { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the value. </summary>
        /// <value> The value. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int Value { get; }

        #endregion Public Properties

        #region Statics

        /// <summary>   URL of the resource. </summary>
        public static readonly CommonRegex Url = new CommonRegex(1,
            @"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[^-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+(:[0-9]+)?|(?:www.|[^-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w_]*)?\??(?:[-\+=&;%@.\w-_]*)#?‌​(?:[\w]*))?)");

        /// <summary>   The email. </summary>
        public static readonly CommonRegex Email =
            new CommonRegex(2, @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}");

        #endregion Statics
    }
}
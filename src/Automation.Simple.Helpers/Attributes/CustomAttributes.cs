namespace Automation.Simple.Helpers.Attributes
{
    using System;

    /// <summary>
    /// Attribute for custom keywords
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    internal class KeywordAttribute : Attribute
    {
        /// <summary>
        /// The _customKeyword represent the keyword associated to a method.
        /// </summary>
        private readonly string _customKeyword;

        internal KeywordAttribute(string keyword)
        {
            _customKeyword = keyword;
        }

        /// <summary>
        /// Overrides in order to send the current keyword
        /// </summary>
        /// <returns>current keyword</returns>
        public override string ToString()
        {
            return _customKeyword;
        }
    }
}

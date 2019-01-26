using Automation.Simple.Helpers.Extensions;
using System;

namespace Automation.Simple.Core.StepDefinitions.DataTransformationTypes
{
    public class StringArgument : IStepArgument
    {
        /// <summary>
        /// The value of input string
        /// </summary>
        private string Value { get; set; }

        /// <summary>
        /// The constructor to instance a new input string object
        /// </summary>
        /// <param name="value">
        /// The initial value
        /// </param>
        public StringArgument(string value)
        {
            Value = value;
        }

        public static implicit operator string(StringArgument inputString)
        {
            return inputString.ToString();
        }


        public static implicit operator StringArgument(string value)
        {
            return new StringArgument(value);
        }

        /// <summary>
        /// Gets the value as string.
        /// </summary>
        /// <returns>The value.</returns>
        public override string ToString()
        {
            try
            {
                return Value.ParseKeyword();
            }
            catch (Exception)
            {
                return Value;
            }
        }
    }
}

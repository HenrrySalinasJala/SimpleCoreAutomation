namespace Automation.Simple.Helpers.Generators
{
    using System;
    using System.Linq;

    public static class StringGenerator
    {
        public const string Alphanumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public const string Alphabetic = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string Numeric = "0123456789";
        public static string GetRandomString(int size = 8, string prefix = null, string suffix = null)
        {
            return GetRandomStringValue(Alphanumeric, size, prefix, suffix);
        }

        /// <summary>This method generates a numeric random string</summary>
        /// <param name="size">size</param>
        /// <param name="prefix">prefix</param>
        /// <param name="suffix">suffix</param>
        /// <returns>Random string generated</returns>
        public static string GetRandomNumericString(int size = 8, string prefix = null, string suffix = null)
        {
            return GetRandomStringValue(Numeric, size, prefix, suffix);
        }

        /// <summary>This method generates a numeric random string within a range</summary>
        /// <param name="minValue">The Min Value</param>
        /// <param name="maxValue">The Max Value</param>
        /// <returns>The Random Value</returns>
        public static string GetRandomNumericString(int minValue, int maxValue)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            return random.Next(minValue, maxValue).ToString();
        }

        /// <summary>The get alphabetic value.</summary>
        /// <param name="size">The size.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="suffix">The suffix.</param>
        /// <returns>Random string generated.</returns>
        public static string GetAlphabeticValue(int size = 8, string prefix = null, string suffix = null)
        {
            return GetRandomStringValue(Alphabetic, size, prefix, suffix);
        }

        /// <summary>Gets a Random String value with a Base String defined</summary>
        /// <param name="baseString">The Base String to generate the random value</param>
        /// <param name="size">The Size of the random value</param>
        /// <param name="prefix">The Prefix value put at the beginning of the value</param>
        /// <param name="suffix">The Suffix value put at the end of the value</param>
        /// <returns>Return a random string value</returns>
        private static string GetRandomStringValue(string baseString, int size, string prefix, string suffix)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            var result = 
                new string(Enumerable.Repeat(baseString, size).Select(s => s[random.Next(s.Length)]).ToArray());
            if (prefix != null)
                result = prefix.Trim() + result;
            if (suffix != null)
                result = result + suffix.Trim();
            return result;
        }
    }
}

namespace Automation.Simple.Helpers.Extensions
{
    
    using Attributes;
    using Generators;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System;
    using Automation.Simple.Core.Environment;

    /// <summary>
    /// The string extension.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// The Numeric Abbreviation string value
        /// </summary>
        private static string NumericAbbreviation = "num";

        /// <summary>
        /// The Alphabetic string value
        /// </summary>
        private static string Alphabetic = "letter";

        /// <summary>
        /// The Space string value
        /// </summary>
        private static string Space = " ";

        /// <summary>
        /// Add a currency sign at the beginning of the value
        /// <para> E.g. $100 </para>
        /// </summary>
        /// <param name="value"> The value to add the currency sign </param>
        /// <returns> The value with currency sign </returns>
        public static string AddCurrencySign(this string value)
        {
            return !value.Contains("$") ? string.Format("${0}", value) : value;
        }

        /// <summary>
        /// Adds a Decimals after the value
        /// <para> E.g. 100.00 </para>
        /// </summary>
        /// <param name="value"> The value to add the decimals </param>
        /// <returns> The value with the decimals </returns>
        public static string AddDecimals(this string value)
        {
            return !value.Contains(".") ? string.Format("{0}.00", value) : value;
        }

        /// <summary>
        /// The to percentage returns a value in format 0.0000 %.
        /// </summary>
        /// <param name="value"> The value. </param>
        /// <returns> The <see cref="string"/>. </returns>
        public static string ToPercentage(this string value)
        {
            return string.Format("{0}.0000 %", value);
        }

        /// <summary>
        /// The remove characters returns a value without arguments sent.
        /// </summary>
        /// <param name="value"> The value with the characters to remove. </param>
        /// <param name="characters"> The characters to remove from the value. </param>
        /// <returns> The <see cref="string"/>. </returns>
        public static string RemoveCharacters(this string value, params string[] characters)
        {
            foreach (var character in characters)
            {
                value = value.Replace(character, string.Empty);
            }

            return value;
        }

        /// <summary>
        /// Returns date normalized if is possible for string like: 'current_date' and 'today'
        /// </summary>
        /// <param name="value"> The value to formate the date. </param>
        /// <returns> The <see cref="string"/>. </returns>
        [Keyword("today")]
        public static string NormalizeDate(this string value)
        {
            if (value.ToLower().Contains("[today"))
            {
                double numberToAdd = 0;
                if (value.Contains("+"))
                    numberToAdd = Int16.Parse(value.Substring(value.IndexOf("+") + 1).RemoveCharacters("]"));
                else if (value.Contains("-"))
                    numberToAdd = Int16.Parse(value.Substring(value.IndexOf("-")).RemoveCharacters("]"));
                return string.Format(ExtensionConstans.DateStyleMonthDayYear, DateTime.Now.AddDays(numberToAdd));
            }
            return value;
        }

        /// <summary>
        /// Returns year keyword normalized.
        /// </summary>
        /// <param name="value"> The value to formate the date. </param>
        /// <example>
        /// [YYYY] returns 2018
        /// [YYYY+1] returns 2018+1=2019
        /// [YYYY-1] returns 2018-1=2017
        /// </example>
        /// <returns> The <see cref="string"/>. </returns>
        [Keyword("yyyy")]
        public static string NormalizeYear(this string value)
        {
            if (value.ToLower().Contains("[yyyy"))
            {
                double numberToAdd = 0;
                if (value.Contains("+"))
                {
                    numberToAdd = Int16.Parse(value.Substring(value.IndexOf("+") + 1).RemoveCharacters("]"));
                }
                else if (value.Contains("-"))
                {
                    numberToAdd = Int16.Parse(value.Substring(value.IndexOf("-")).RemoveCharacters("]"));
                }
                var currentYear = DateTime.Now;
                return currentYear.AddYears(Convert.ToInt16(numberToAdd)).Year.ToString();
            }
            return value;
        }

        /// <summary>
        /// Returns the value with the given Date Format
        /// </summary>
        /// <param name="value"> The Date text </param>
        /// <param name="format"> The Format for the Date </param>
        /// <returns> The value with the Date Format applied </returns>
        public static string ApplyDateFormat(this string value, string format = "M/d/yyyy")
        {
            var date = value.Split('/');
            return new DateTime(Int32.Parse(date[2]), Int32.Parse(date[0]), Int32.Parse(date[1])).ToString(format);
        }

        /// <summary>
        /// Returns the value with the given date formats
        /// </summary>
        /// <param name="value">Date text</param>
        /// <param name="fromFormat">Original format</param>
        /// <param name="toFormat">Final format</param>
        /// <returns>Date on the final format</returns>
        public static string ApplyDateFormat(this string value, string fromFormat, string toFormat)
        {
            DateTime date;
            if (DateTime.TryParseExact(value, fromFormat, CultureInfo.CurrentCulture, DateTimeStyles.AdjustToUniversal, out date))
            {
                return date.ToString(toFormat);
            }
            else
            {
                Console.Error.WriteLine("There was an exception parsing: {0}", value);
                return value;
            }
        }

        /// <summary>
        /// Returns the value with a currency format ($ symbol and two decimals)
        /// </summary>
        /// <param name="value">Value as text</param>
        /// <returns>String on currency format</returns>
        public static string ApplyCurrencyFormat(this string value)
        {
            try
            {
                var numericValue = double.Parse(value);
                return string.Format("{0:C}", numericValue);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("There was an exception parsing: {0}. Error {1}", value, e.Message);
                return value;
            }
        }
        
        /// <summary>
        /// Converts the input value from currency type to decimal type
        /// </summary>
        /// <param name="value">input value</param>
        /// <returns>money's value as decimal type</returns>
        public static decimal CurrencyToDecimal(this string value)
        {
            try
            {
                return decimal.Parse(value, NumberStyles.Currency);
            }
            catch (FormatException)
            {
                Console.Error.WriteLine("There was an exception parsing: {0}", value);
                return 0;
            }
        }
        
        /// <summary>
        /// Return hexadecimal format value of a rgb color
        /// </summary>
        /// <param name="rgbColor">rgb color format</param>
        /// <returns>hexadecimal color value</returns>
        public static string ToHexColorFormat(this string rgbColor)
        {
            string[] numbersColor = rgbColor.RemoveCharacters("rgba(", ")").Split(',');
            int r = Int32.Parse(numbersColor[0].Trim());
            int g = Int32.Parse(numbersColor[1].Trim());
            int b = Int32.Parse(numbersColor[2].Trim());
            return "#" + r.ToString("X") + g.ToString("X") + b.ToString("X");
        }        

        /// <summary>
        /// Validates the input value replacing by a random value
        /// </summary>
        /// <param name="value">string value to be parsed</param>
        /// <returns>The value without tag and defined values</returns>
        [Keyword("random")]
        public static string NormalizeRandom(this string value)
        {
            if (value.ToLower().Contains("[random"))
            {
                string[] substrs = value.Split('[');
                string randomPart = substrs[1];
                if (randomPart.Contains("+") && randomPart.Contains("]"))
                {
                    int quantity =
                        Int16.Parse(randomPart
                        .Substring(randomPart.IndexOf("+"), randomPart.IndexOf("]") - randomPart.IndexOf("+")));
                    return GenerateString(randomPart, quantity, substrs[0]) + randomPart.Split(']').Last();
                }
                return GenerateString(randomPart, 4, substrs[0]);
            }
            return value;
        }

        /// <summary>
        /// Generates the string random value given a criteria
        /// </summary>
        /// <param name="value">The string value to be replaced</param>
        /// <param name="quantity">The amount characters</param>
        /// <param name="subValue">The prefix string value</param>
        /// <returns></returns>
        private static string GenerateString(string value, int quantity, string subValue)
        {
            if (value.ToLower().Contains(NumericAbbreviation))
            {
                return StringGenerator.GetRandomNumericString(quantity, subValue);
            }
            else if (value.ToLower().Contains(Alphabetic))
            {
                return StringGenerator.GetAlphabeticValue(quantity, subValue);
            }
            return StringGenerator.GetRandomString(quantity, subValue);
        }

        /// <summary>
        /// Verifies if the input value contain empty option
        /// </summary>
        /// <param name="value">string to be parsed</param>
        /// <returns>empty string if was validated, no changes otherwise</returns>
        [Keyword("empty")]
        public static string NormalizeEmpty(this string value)
        {
            if (value.ToLower().Contains("[empty]"))
                return String.Empty;
            return value;
        }

        /// <summary>
        /// Validates if string contains values to be parsed
        /// </summary>
        /// <param name="value">string value</param>
        /// <returns>parsed string, no changes otherwise</returns>
        public static string ParseKeyword(this string value)
        {
            if (ContainsKeyword(value))
            {
                var keyword = GetKeyword(value);
                string parsedKeyword = InvokeParserMethod(keyword);
                if (keyword != parsedKeyword)
                    return ParseKeyword(value.Replace(keyword, parsedKeyword));
            }
            return value;
        }

        /// <summary>
        /// Returns the first match keyword with brackets
        /// </summary>
        /// <param name="value">string value</param>
        /// <returns>keyword with brackets</returns>
        private static string GetKeyword(string value)
        {
            if (!string.IsNullOrEmpty(value) && ContainsBrackets(value))
            {
                string subValueWithKeyword = value.Substring(value.IndexOf("["));
                string keyword = 
                    subValueWithKeyword.Substring(subValueWithKeyword.IndexOf("["), 
                    subValueWithKeyword.IndexOf("]") - subValueWithKeyword.IndexOf("[") + 1);
                if (!ContainsKeyword(keyword))
                    return GetKeyword(subValueWithKeyword.Substring(subValueWithKeyword.IndexOf("]") + 1));
                return keyword;
            }
            return value;
        }

        /// <summary>
        /// Verifies if the value contains possibles keywords
        /// </summary>
        /// <param name="value">argument string</param>
        /// <returns>true if contains, false otherwise</returns>
        private static bool ContainsKeyword(string value)
        {
            if (ContainsBrackets(value))
            {
                var memberInfo = typeof(StringExtension);
                return memberInfo.GetMethods()
                    .Any(method => method.GetCustomAttributes(true)
                    .OfType<KeywordAttribute>()
                    .Any(attr => value.ToLower().Contains(attr.ToString())));
            }
            return false;
        }

        /// <summary>
        /// Gets and execute the correct parser method 
        /// </summary>
        /// <param name="value">The keyword to parse</param>
        /// <returns>The parsed string value</returns>
        private static string InvokeParserMethod(string value)
        {
            var memberInfo = typeof(StringExtension);
            var methodToExe = memberInfo.GetMethods()
                .Where(m => m.GetCustomAttributes(typeof(KeywordAttribute), false).Length > 0)
                .Single(method => method.GetCustomAttributes(true)
                    .Any(attr => value.ToLower().Contains(attr.ToString())));

            var parsedValue = methodToExe.Invoke(null, new string[] { value });            
            return parsedValue.ToString();
        }

        /// <summary>
        /// Verifies if the value contains brackets to be a possible keyword
        /// </summary>
        /// <param name="value">string value</param>
        /// <returns>true if contains, false otherwise</returns>
        private static bool ContainsBrackets(string value)
        {
            return value.Contains("[") && 
                value.Substring(value.IndexOf("[")).Contains("]");
        }

        /// <summary>
        /// Split words by camel case.
        /// </summary>
        /// <param name="value">string value</param>
        /// <returns>string with words separated by space</returns>
        public static string SplitCamelCaseWords(this string value)
        {
            const string splitCamelCasePattern = @"(?<!^)([A-Z0-9][a-z]|(?<=[a-z])[A-Z0-9])";
            const string moreThanOneSpacePattern = @"\s+";
            string result = Regex.Replace(
                value, splitCamelCasePattern, " $1", RegexOptions.Compiled).Trim();
            return Regex.Replace(result, moreThanOneSpacePattern, Space).Trim();
        }

        /// <summary>
        /// Removes all spaces within a string.
        /// </summary>
        /// <param name="value">string to be modified</param>
        /// <returns>New string without spaces</returns>
        public static string RemoveSpaces(this string value)
        {
            return value.Replace(" ", "");
        }

        /// <summary>
        /// Gets the First Name of the user configured in the Config file
        /// </summary>
        /// <param name="value">string to be parsed</param>
        /// <returns> First Name if was validated, no changes otherwise</returns>
        [Keyword("firstname")]
        public static string NormalizeUserName(this string value)
        {
            if (value.ToLower().Contains("[firstname]"))
            {
                return Config.UserForWebApp;
            }
            return value;
        }
        /// <summary>
        /// Replaces the keyword by a new line (/r/n)
        /// </summary>
        /// <param name="value">String to be parsed</param>
        /// <returns>The value with a new line instead of the keyword.</returns>
        [Keyword("newline")]
        public static string NormalizeNewLine(this string value)
        {
            if (value.ToLower().Contains("[newline]"))
            {
                return Regex.Replace(value, @"(\[newline\])", "\r\n", RegexOptions.IgnoreCase);
            }
            return value;
        }
    }    
}

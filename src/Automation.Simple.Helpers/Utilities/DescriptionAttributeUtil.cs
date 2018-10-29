namespace Automation.Simple.Helpers.Utilities
{
    using log4net;
    using System;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// Description attribute utility.
    /// </summary>
    public static class DescriptionAttributeUtil
    {
        /// <summary>
        /// The logger instance.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get Enum value given the Description attribute value.
        /// </summary>
        /// <typeparam name="T">The enum.</typeparam>
        /// <param name="descriptionValue">The Description value.</param>
        /// <returns>The Enum value.</returns>
        public static T GetValueFromDescription<T>(string descriptionValue)
        {
            log.Info($"Get value: {descriptionValue} from description attribute");
            var type = GetEnumType<T>();

            var fields =
                type.GetFields()
                    .SelectMany(f => f.GetCustomAttributes(typeof(DescriptionAttribute), false),
                        (field, attribute) => new { Field = field, Attribute = attribute })
                    .Where(
                        attribute =>
                            ((DescriptionAttribute)attribute.Attribute).Description.Split(';')
                                .Any(value => value.Equals(descriptionValue))).ToList();

            if (!fields.Any())
            {
                log.Error($"Value: {descriptionValue}, not found in any description attribute");
                throw new ArgumentException($"There is no a value with the description: '{descriptionValue}'.");
            }

            if (fields.Count > 1)
            {
                log.Error($"Value: {descriptionValue}, multiple matches {string.Join(",", fields)}");
                throw new Exception($"There are multiple values with the same description: '{descriptionValue}'.");
            }

            return (T)fields.First().Field.GetRawConstantValue();
        }

        /// <summary>
        /// Gets Description attribute values from the given Enum.
        /// </summary>
        /// <typeparam name="T">The enum.</typeparam>
        /// <returns>An array with the Description values.</returns>
        public static string[] GetDescriptionValues<T>()
        {
            var type = GetEnumType<T>();

            var descriptionAttributes =
                type.GetFields().SelectMany(field => field.GetCustomAttributes(typeof(DescriptionAttribute), false));

            return descriptionAttributes.Select(enumValue => ((DescriptionAttribute)enumValue).Description).ToArray();
        }

        /// <summary>
        /// Gets the Enum type.
        /// </summary>
        /// <typeparam name="T">The enum.</typeparam>
        /// <returns>The type of the enum.</returns>
        private static Type GetEnumType<T>()
        {
            var type = typeof(T);

            if (!type.IsEnum)
                throw new ArgumentException("{0} isn't an enum.", type.Name);

            return type;
        }
    }
}

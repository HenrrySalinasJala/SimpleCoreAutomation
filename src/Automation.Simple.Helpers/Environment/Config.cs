namespace Automation.Simple.Core.Environment
{
    using log4net;
    using NUnit.Framework;
    using System;
    using System.Configuration;

    /// <summary>
    /// Environment Configuration
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// The logger instance.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The retry times number.
        /// </summary>
        public static int RetryTimes = GetInt(Keys.RetryTimes, 1);

        /// <summary>
        /// The implicit timeout in seconds.
        /// </summary>
        public static int ImplicitTimeoutInSeconds = GetInt(Keys.ImplicitTimeoutInSeconds, 30);

        /// <summary>
        /// The explicit timeout in seconds.
        /// </summary>
        public static int ExplicitTimeoutInSeconds = GetInt(Keys.ExplicitTimeoutInSeconds, 60);

        /// <summary>
        /// The wait interval in milliseconds.
        /// </summary>
        public static int WaitIntervalInMilliseconds = GetInt(Keys.WaitIntervalInMilliseconds, 500);

        /// <summary>
        /// The user for the web app.
        /// </summary>
        public static string UserForWebApp = GetString(Keys.UserForWebApp, "TestUser");

        /// <summary>
        /// The password for the web app.
        /// </summary>
        public static string PasswordForWebApp = GetString(Keys.PasswordForWebApp, "TestPwd!23");

        /// <summary>
        /// The Web App URL.
        /// </summary>
        public static string WebAppUrl = GetString(Keys.WebAppUrl, "https://google.com/");

        /// <summary>
        /// Gets value as a string.
        /// </summary>
        /// <param name="keyName">key name as a string.</param>
        /// <param name="defaultValue">default value as a string.</param>
        /// <returns>Returns the value as a string</returns>
        public static string GetString(string keyName, string defaultValue)
        {
            if (!string.IsNullOrEmpty(TestContext.Parameters.Get(keyName)))
            {
                return TestContext.Parameters.Get(keyName);
            }
            else if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[keyName]))
            {
                return ConfigurationManager.AppSettings[keyName];
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Gets an int value from the configuration or the NUnit parameter.
        /// </summary>
        /// <param name="configurationPropertyName">The configuration name.</param>
        /// <param name="defaultValue">The default value if the configuration is not found.</param>
        /// <returns>returns .</returns>
        private static int GetInt(string configurationPropertyName, int defaultValue)
        {
            if (!string.IsNullOrEmpty(TestContext.Parameters.Get(configurationPropertyName)))
            {
                return int.Parse(TestContext.Parameters.Get(configurationPropertyName));
            }
            else if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[configurationPropertyName]))
            {
                try
                {
                    return int.Parse(ConfigurationManager.AppSettings[configurationPropertyName]);
                }
                catch (Exception)
                {
                    Console.Error.WriteLine($"Invalid value for property: [{configurationPropertyName}]");
                    return defaultValue;
                }
            }
            else
            {
                return defaultValue;
            }
        }
    }
}

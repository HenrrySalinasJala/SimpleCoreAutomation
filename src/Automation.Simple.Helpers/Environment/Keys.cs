namespace Automation.Simple.Core.Environment
{
    /// <summary>
    /// This struct contains the common keys.
    /// </summary>
    public struct Keys
    {
        /// <summary>
        /// Key for storing the Wait Interval in milliseconds.
        /// </summary>
        public const string WaitIntervalInMilliseconds = "waitIntervalInMilliseconds";

        /// <summary>
        /// Key for storing the Explicit timeout Interval in seconds.
        /// </summary>
        public const string ExplicitTimeoutInSeconds = "explicitTimeoutInSeconds";

        /// <summary>
        /// Key for storing the Implicit timeout Interval in seconds.
        /// </summary>
        public const string ImplicitTimeoutInSeconds = "implicitTimeoutInSeconds";

        /// <summary>
        /// Key value for test retry times.
        /// </summary>
        public const string RetryTimes = "retryTimes";

        /// <summary>
        /// key Web App URL
        /// </summary>
        public const string WebAppUrl = "webAppUrl";

        /// <summary>
        /// key user id
        /// </summary>
        public const string UserForWebApp = "userForWebApp";

        /// <summary>
        /// key password
        /// </summary>
        public const string PasswordForWebApp = "passwordForWebApp";

        /// <summary>
        /// key web browser
        /// </summary>
        public const string Browser = "browser";
    }
}

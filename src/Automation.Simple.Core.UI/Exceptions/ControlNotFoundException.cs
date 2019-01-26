namespace Automation.Simple.Core.UI.Exceptions
{
    using log4net;
    using System;

    /// <summary>
    /// Represents errors that occur when a control wasn't found.
    /// </summary>
    [Serializable]
    public class ControlNotFoundException : Exception
    {
        /// <summary>
        /// The logger instance.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public ControlNotFoundException(string message) : base(message)
        {
            log.Error($"Unable to find control {message}");
        }
    }
}

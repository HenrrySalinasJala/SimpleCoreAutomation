namespace Automation.Simple.Core.UI.Actions.Exceptions
{
    using log4net;
    using System;

    [Serializable]
    public class ControlActionException : Exception
    {

        /// <summary>
        /// The logger instance.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public ControlActionException(string message) : base(message)
        {
            log.Error(message);
        }
    }
}

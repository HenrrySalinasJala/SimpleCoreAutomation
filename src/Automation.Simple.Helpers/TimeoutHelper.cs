namespace Automation.Simple.Helpers
{
    using log4net;
    using System;
    using System.Threading;

    /// <summary>
    /// Custom timeout helper.
    /// </summary>
    public class TimeoutHelper
    {
        /// <summary>
        /// The logger instance.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Creates an instance of <see cref="TimeoutHelper"/> with default timeout and waitInterval.
        /// </summary>
        public TimeoutHelper()
            : this(timeout: TimeSpan.FromSeconds(30), waitInterval: TimeSpan.FromMilliseconds(200))
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="TimeoutHelper"/> with the specified timeout and default waitInterval.
        /// </summary>
        /// <param name="timeout">The duration after which to stop waiting.</param>
        public TimeoutHelper(TimeSpan timeout)
            : this(timeout: timeout, waitInterval: TimeSpan.FromMilliseconds(200))
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="TimeoutHelper"/> with the specified timeout and waitInterval.
        /// </summary>
        /// <param name="timeout">The duration after which to stop waiting.</param>
        /// <param name="waitInterval">The length of time to wait between retries.</param>
        public TimeoutHelper(TimeSpan timeout, TimeSpan waitInterval)
        {
            Timeout = timeout;
            WaitInterval = waitInterval;
        }

        /// <summary>
        /// Gets or sets the Timeout.
        /// </summary>
        public TimeSpan Timeout { get; protected set; }

        /// <summary>
        /// Gets or sets the WaitInterval.
        /// </summary>
        public TimeSpan WaitInterval { get; protected set; }

        /// <summary>
        /// Waits for the given condition function to return true, or until the timeout is reached.
        /// </summary>
        /// <param name="conditionChecker">The condition function to check.</param>
        /// <exception cref="TimeoutException">The condition was not met before the timeout was reached.</exception>
        /// <returns>True if the condition succeeds i.e., false otherwise.</returns>
        public bool WaitFor(Func<bool> conditionChecker)
        {
            var startTime = DateTime.Now;
            log.Info("Waiting for condition");
            var waitIntervalMilliseconds = Convert.ToInt32(WaitInterval.TotalMilliseconds);
            do
            {
                if (conditionChecker())
                {
                    return true;
                }
                Thread.Sleep(waitIntervalMilliseconds);
            }
            while (DateTime.Now - startTime < Timeout);
            return false;
        }
    }
}

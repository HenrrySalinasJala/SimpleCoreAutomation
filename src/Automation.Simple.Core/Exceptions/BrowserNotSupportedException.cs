namespace Automation.Simple.Core.Exceptions
{
    using log4net;
    using System;

    public class BrowserNotSupportedException : Exception
    {
        protected static readonly ILog log = LogManager.GetLogger(System.Reflection
                                                                        .MethodBase
                                                                        .GetCurrentMethod()
                                                                        .DeclaringType);

        public BrowserNotSupportedException(string message) : base(message)
        {
            log.Error($"There was an error trying to initialize a browser driver {message}");
        }
    }
}

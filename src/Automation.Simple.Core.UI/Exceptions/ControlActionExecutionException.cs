using Automation.Simple.Core.UI.Enums;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Simple.Core.UI.Exceptions
{
    public class ControlActionExecutionException: Exception
    {
        /// <summary>
        /// The logger instance.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ControlActionExecutionException(string controlName,ControlType controlType,
            string message) : base(message)
        {
            log.Error($"Unable to execute control action in {controlName} {controlType}" +
                $"error [{message}] ");
        }
    }
}

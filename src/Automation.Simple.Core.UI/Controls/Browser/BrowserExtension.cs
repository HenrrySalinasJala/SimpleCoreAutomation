using Automation.Simple.Core.Environment;
using Automation.Simple.Helpers;
using log4net;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Simple.Core.UI.Controls.Browser
{
    public class BrowserExtension
    {
        /// <summary>
        /// The logger instance.
        /// </summary>
        protected static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        internal static object ExecuteScript(IWebDriver driver, string script, params object[] args)
        {
            try
            {
                var javascriptExecutor = driver as IJavaScriptExecutor;
                var result = javascriptExecutor.ExecuteScript(script, args);
                log.Debug($"Execute the script '{script}'. Result returned: [{result?.ToString()}].");
                return result;
            }
            catch (Exception error)
            {
                log.Debug($"Unable to exeucte the script '{script}'. Error [{error.Message}].");
                return null;
            }
        }

        /// <summary>
        /// Waits for all Angular pending requests to complete.
        /// </summary>
        /// <param name="secondsToWait">The duration after which to stop waiting.</param>
        public static void WaitForAngular(IWebDriver driver, int secondsToWait)
        {
            if (!(driver == null))
            {
                var timeoutInSeconds = TimeSpan.FromSeconds(secondsToWait);
                var waitIntervalInMilliseconds = TimeSpan.FromMilliseconds(Config.WaitIntervalInMilliseconds);
                var timeoutHelper = new TimeoutHelper(timeoutInSeconds, waitIntervalInMilliseconds);
                log.Debug("Waiting for Angular pending requests to complete.");

                var conditionSucceeded = timeoutHelper.WaitFor(() =>
                {
                    var pendingRequests = GetAngularPendingRequests(driver);
                    if (pendingRequests <= 0)
                    {
                        return true;
                    }
                    return false;
                });

                if (!conditionSucceeded)
                {
                    log.Debug($"Angular pending requests not completed within {timeoutInSeconds} seconds");
                }
            }
        }

        /// <summary>
        /// Gets all the Angular pending requests.
        /// </summary>
        /// <returns>The angular pending requests.</returns>
        private static int GetAngularPendingRequests(IWebDriver driver)
        {
            try
            {
                return Convert.ToInt32(ExecuteScript(driver,
                        @"return angular.element(document.body).injector().get('$http').pendingRequests.length;"));
            }
            catch (InvalidOperationException)
            {
                return -1;
            }
        }
    }
}

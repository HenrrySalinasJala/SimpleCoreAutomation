namespace Automation.Simple.Core.UI.Actions.Control
{
    using Automation.Simple.Core.Selenium;
    using log4net;
    using OpenQA.Selenium;

    public class BrowserActions
    {
        /// <summary>
        /// The logger instance.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void GoTo(string url)
        {
            DriverManager.GetInstance().InitWebDriver();
            log.Info($"Go to '{url}' URL.");
            IWebDriver Driver = DriverManager.GetInstance().GetDriver();
            Driver.Navigate().GoToUrl(url);
        }

        public void CloseBrowser()
        {
            DriverManager.GetInstance().QuitDriver();
        }

    }
}

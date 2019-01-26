using Automation.Simple.Core.Environment;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Automation.Simple.Core.Selenium
{
    public class DriverManager
    {
        /// <summary>
        /// The logger instance.
        /// </summary>
        protected static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static DriverManager _instance;
        private IWebDriver _driver;
        private WebDriverWait _wait;

        private DriverManager()
        {
        }

        public static DriverManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DriverManager();
            }
            return _instance;
        }

        public void InitWebDriver()
        {
            log.Info("Initializing Driver");
            if (_driver==null)
            {
                _driver = DriverFactory.GetDriver(Config.Browser).InitDriver();
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Config.ImplicitTimeoutInSeconds);
                _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(Config.ExplicitTimeoutInSeconds);
                _driver.Manage().Window.Maximize();
                _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(Config.ExplicitTimeoutInSeconds));
            }
        }

        public IWebDriver GetDriver()
        {
            log.Info("Getting Driver");
            return _driver;
        }

        public WebDriverWait GetWait()
        {
            return _wait;
        }

        public void QuitDriver()
        {
            log.Info("Quit Driver");
            _driver.Quit();
        }

        /// <summary>
        /// Takes a screenshot as base64.
        /// </summary>
        /// <returns>The screenshot as base64.</returns>
        public string TakeScreenshotAsBase64()
        {
            var takesScreenshot = _driver as ITakesScreenshot;
            try
            {
                log.Info("Take screenshot as Base64");
                var screenshot = takesScreenshot.GetScreenshot();
                return screenshot.AsBase64EncodedString;
            }
            catch (Exception e)
            {
                log.Warn($"Unable to take the screenshot as Base64. Error [{e.Message}].");
                return null;
            }
        }
    }
}

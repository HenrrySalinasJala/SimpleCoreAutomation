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
            //InitWebDriver();

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
                _driver = DriverFactory.GetDriver("Chrome").InitDriver();
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
                _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
                _driver.Manage().Window.Maximize();
                _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(40));
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
    }
}

namespace Automation.Simple.Core.Selenium
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Firefox;

    internal class Firefox : IDriver
    {
        public IWebDriver InitDriver()
        {
            return new FirefoxDriver();
        }
    }
}
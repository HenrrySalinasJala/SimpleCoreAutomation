namespace Automation.Simple.Core.Selenium
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    /// <summary>
    /// The implementation for chrome browser.
    /// </summary>
    internal class Chrome : IDriver
    {
        /// <summary>
        /// Sets local configuration for chrome browser.
        /// </summary>
        /// <returns>The selenium driver instance for chrome driver.</returns>
        public IWebDriver InitDriver()
        {
            var chromeOptions = GetChromeOptions();
            return new ChromeDriver(chromeOptions);
        }

        
        /// <summary>
        /// Gets the custom options for chrome driver.
        /// </summary>
        /// <returns>The options for chrome driver.</returns>
        private ChromeOptions GetChromeOptions()
        {
            ChromeOptions chromeOptions = new ChromeOptions();

            //Boolean which specifies whether we should ask the user if we should download a file (true)
            chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);

            //String which specifies where to download files to by default.
            chromeOptions.AddUserProfilePreference("download.default_directory", "C:\\downloads");

            //Prevents certain types of downloads based on integer value - No special restrictions = 0
            chromeOptions.AddUserProfilePreference("download_restrictions", 0);

            //Allows multiple (automatic) downloads
            chromeOptions
                .AddUserProfilePreference("profile.content_settings.pattern_pairs.*.multiple-automatic-downloads", 1);

            return chromeOptions;
        }
    }
}
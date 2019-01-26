namespace Automation.Simple.Core.Selenium
{
    using Automation.Simple.Core.Enums;
    using Automation.Simple.Core.Exceptions;
    using Automation.Simple.Helpers.Utilities;
    using System;

    public static class DriverFactory
    {
        public static IDriver GetDriver(string browser)
        {
            var browserType = GetRequestAction(browser);
            switch (browserType)
            {
                case BrowserType.Chrome:
                    return new Chrome();
                case BrowserType.Firefox:
                    return new Firefox();
                default:
                    throw new BrowserNotSupportedException($"browser {browser} not supported");
            }
        }

        private static BrowserType GetRequestAction(string requestActionName)
        {
            try
            {
                return DescriptionAttributeUtil.GetValueFromDescription<BrowserType>(requestActionName);
            }
            catch (Exception error)
            {
                throw new Exception($"Unknown browser {requestActionName}, error {error.Message}");
            }
        }
    }
}

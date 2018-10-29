
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Simple.Core.Selenium
{
    public static class DriverFactory
    {
        public static IDriver GetDriver(string browser)
        {
            switch (browser)
            {
                case "Chrome":
                    return new Chrome();
                case "Firefox":
                    return new Firefox();
                default:
                    throw new Exception($"browser {browser} not supported");
            }
        }
    }
}

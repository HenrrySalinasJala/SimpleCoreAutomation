using Automation.Simple.Core.Selenium;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Simple.Core.Test.Integration
{
    [SetUpFixture]
    public class SetUpFixture:BaseTest
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var indexFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "Resources", "Integration", "index.html");
            var fileprotocol = $"file:///{indexFile}";
            BrowserAction.GoTo(fileprotocol);

        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            DriverManager.GetInstance().QuitDriver();
        }
    }
}

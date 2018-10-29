using Automation.Simple.Core.UI.Controls;
using Automation.Simple.Core.UI.Controls.Button;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Simple.Core.Test.Controls
{
    [TestFixture]
    public class ButtonTest
    {
        private readonly string _resourcesPath = AppDomain.CurrentDomain.BaseDirectory + @"\Resources\Controls\";

        [TestCase(typeof(Button), "Cancel", "Controls.html", TestName = "ControlFinder_FindControl: Returns Button control with the 'button' attribute")]
        public void ControlFinder_FindControl_ReturnsCorrectControl(Type expectedType, string controlName, string htmlResourceName)
        {
            string resourcePath = Path.Combine(_resourcesPath, htmlResourceName);
            string source = File.ReadAllText(resourcePath);

            var controlFinder = new ControlFinder("");
            var webControl = controlFinder.FindControl(source, controlName);
            Assert.IsInstanceOf(expectedType, webControl);
        }
    }
}
